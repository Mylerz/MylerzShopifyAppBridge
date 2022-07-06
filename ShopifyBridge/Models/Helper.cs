using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using NLog;

namespace ShopifyBridge.Models
{
    public static class Helper
    {
        private static (List<List<OrderModel>>, List<List<RequestPackage>>) ConvertOrder(List<ShopifyOrder> orders)
        {
            var Mylerzorders = orders.SelectMany((order, index) =>
            {
                return order.current_line_items.GroupBy(item => item.warehouse).Select(s => s.ToList()).Select(warehouseLineItems =>
                  {
                      return new OrderModel()
                      {
                          WarehouseName = warehouseLineItems.FirstOrDefault()?.warehouse,
                          PickupDueDate = order.closed_at.Hour < 12 ? order.closed_at : order.closed_at.AddDays(1),
                          Package_Serial = index,
                          Reference = order.name,
                          Description = warehouseLineItems.Select(lineItem => $"Title: {lineItem.title} ( {lineItem.sku} ), Quantity: {lineItem.quantity}").Aggregate((firstItem, secondItem) => firstItem + "\n" + secondItem),
                          Total_Weight = warehouseLineItems.Sum(item=>item.grams/1000),
                          Service_Type = Service_Type.DTD.ToString(),
                          Service = Service.ND.ToString(),
                          Service_Category = Service_Category.DELIVERY.ToString(),
                          Payment_Type = order.financial_status == "paid" ? Payment_Type.PP.ToString() : Payment_Type.COD.ToString(),
                          COD_Value = order.financial_status == "paid" ? 0 : warehouseLineItems.Sum(line_item => double.Parse(line_item.price) * line_item.quantity + double.Parse(line_item.tax_lines.FirstOrDefault()?.price) - double.Parse(line_item.total_discount)),
                          Pieces = new List<PieceDTO>() { new PieceDTO(1, order.note) },
                          Customer_Name = order.shipping_address.name,
                          Mobile_No = order.shipping_address.phone == null ? order.customer.phone : order.shipping_address.phone,
                          Street = order.shipping_address.address1,
                          Country = order.shipping_address.country,
                          Neighborhood = order.shipping_address.city,
                          Address_Category = Address_Category.H.ToString(),
                          Currency = order.currency
                      };
                  }).ToList();
            }).ToList();

            var ListOfPackages = orders.SelectMany((order) =>
            {
                return order.current_line_items.GroupBy(item => item.warehouse).ToList().Select(warehouseLineItems =>
                {
                    return new RequestPackage()
                    {
                        LineItems = warehouseLineItems.Select(s=>s).ToList(),
                        Warehouse = warehouseLineItems.Select(l=>l.warehouse).FirstOrDefault()
                    };
                }).ToList();
            }).ToList();

            var pickupOrderPerWarehouse = Mylerzorders.GroupBy(pickupOrder => pickupOrder.WarehouseName).ToList().Select(s => s.ToList()).ToList();

            var packagesPerWarehouse = ListOfPackages.GroupBy(package => package.Warehouse).ToList().Select(s => s.ToList()).ToList();

            return (pickupOrderPerWarehouse, packagesPerWarehouse);
        }

        private static async Task<string> Post(string url, string order, string accessToken)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add(HttpRequestHeader.Authorization.ToString(), $"bearer {accessToken}");

            var requestBody = new StringContent(order, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, requestBody);
            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }

        public static async Task<APIResponse> FulfillOrders(List<ShopifyOrder> orders, string token, string url, Logger logger)
        {
            (List<List<OrderModel>>, List<List<RequestPackage>>) OrderPackagesTuple = (null, null);

            try
            {
                 OrderPackagesTuple = ConvertOrder(orders);
                logger.Info($"{DateTime.Now}:  Constructed Order Successfully {OrderPackagesTuple}");
            }
            catch (Exception ex)
            {

                logger.Error($"{DateTime.Now}:  Error Constructing Order {ex}");
                return new APIResponse(null, true, $"Error Constructing Order: {ex.Message}",ex.StackTrace);
            }


            var resultArray = OrderPackagesTuple.Item1.Select(mylerzOrder =>
            {
                var requestJson = JsonConvert.SerializeObject(mylerzOrder, Formatting.Indented);
                return Post(url, requestJson, token);

            }).ToList();

            var responseList = await Task.WhenAll(resultArray);

            var responseObjList = responseList.Select(response => JsonConvert.DeserializeObject<AddOrderResponse>(response));

            if (responseObjList.Any(response=>response.IsErrorState==true))
            {
                return new APIResponse(null, true, responseObjList.Where(response=>response.IsErrorState == true).FirstOrDefault().ErrorDescription,"");
            }

            var barcodesPerPackage = responseObjList.SelectMany(response => response.Value.Packages.Select(package => package.BarCode)).ToList();

            var lineItemsPerPackage = OrderPackagesTuple.Item2.SelectMany(requestpackage => requestpackage.Select(package => package.LineItems).ToList()).ToList();

            var barcodesPerLineItemTuple = barcodesPerPackage.Zip(lineItemsPerPackage, (barcode, lineItems) =>
            {
                return lineItems.Select(line => (line, barcode)).ToList();
            }).SelectMany(tup => tup).ToList();

            return new APIResponse(new APIResponseData(barcodesPerLineItemTuple), false, "", "");
        }
    }
}