using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopifyBridge.Models
{
    public class AddOrderResponse
    {
        public ReponseData Value { get; set; }
        public object CoreValue { get; set; }
        public bool IsErrorState { get; set; }
        public string ErrorDescription { get; set; }
        public object ErrorMetadata { get; set; }
        public string Warehouse { get; set; }
    }

    public class ReponseData
    {
        public string PickupOrderCode { get; set; }
        public string OrderName { get; set; }
        public List<Package> Packages { get; set; }
        public int? ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class Package
    {
        public long? packageNo { get; set; }
        public string Reference { get; set; }
        public long? BarCode { get; set; }
        public string Status { get; set; }
        public int? ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

    }

}