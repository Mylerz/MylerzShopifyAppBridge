using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopifyBridge.Models
{

    public class Requst
    {
        public List<ShopifyOrder> orders { get; set; }
        public string token { get; set; }
        //public string warehouse { get; set; }
    }

    public class ShopifyOrder
    {
        public string id;
        public string email;
        public DateTime closed_at;
        public DateTime created_at;
        public DateTime updated_at;
        public int number;
        public string note;
        public string token;
        public string gateway;
        public bool test;
        public string total_price;
        public string total_outstanding;
        public string subtotal_price;
        public int total_weight;
        public string total_tax;
        public bool taxes_included;
        public string currency;
        public string financial_status;
        public bool confirmed;
        public string total_discounts;
        public string total_line_items_price;
        public object cart_token;
        public bool buyer_accepts_marketing;
        public string name;
        public object referring_site;
        public object landing_site;
        public object cancelled_at;
        public object cancel_reason;
        public string total_price_usd;
        public object checkout_token;
        public object reference;
        public long user_id;
        public object location_id;
        public object source_identifier;
        public object source_url;
        public DateTime processed_at;
        public object device_id;
        public object phone;
        public object customer_locale;
        public int app_id;
        public object browser_ip;
        public object landing_site_ref;
        public int order_number;
        //public List<object> discount_applications;
        //public List<object> discount_codes;
        //public List<object> note_attributes;
        public List<string> payment_gateway_names;
        public string processing_method;
        public string checkout_id;
        public string source_name;
        public string fulfillment_status;
        //public List<object> tax_lines;
        public string tags;
        public string contact_email;
        public string order_status_url;
        public string presentment_currency;
        public TotalLineItemsPriceSet total_line_items_price_set;
        //public TotalDiscountsSet total_discounts_set;
        public TotalShippingPriceSet total_shipping_price_set;
        //public SubtotalPriceSet subtotal_price_set;
        //public TotalPriceSet total_price_set;
        //public TotalTaxSet total_tax_set;
        public List<LineItem> line_items;
        public List<LineItem> current_line_items;
        public List<Fulfillment> fulfillments;
        public List<Refund> refunds;
        public string total_tip_received;
        public object original_total_duties_set;
        public object current_total_duties_set;
        public string admin_graphql_api_id;
        public List<object> shipping_lines;
        public BillingAddress billing_address;
        public ShippingAddress shipping_address;
        public Customer customer;
        public string current_total_price;
    }

    public class TotalLineItemsPriceSet
    {
        public ShopMoney shop_money;
        public PresentmentMoney presentment_money;

    }
    public class TotalShippingPriceSet
    {
        public ShopMoney shop_money;
        public PresentmentMoney presentment_money;

    }

    public class ShopMoney
    {
        public string amount;
        public string currency_code;

    }
    public class PresentmentMoney
    {
        public string amount;
        public string currency_code;

    }

    public class LineItem
    {
        public string warehouse;
        public string id;
        public string variant_id;
        public string title;
        public int quantity;
        public string sku;
        public string variant_title;
        public string vendor;
        public string fulfillment_service;
        public string product_id;
        public bool requires_shipping;
        public bool taxable;
        public bool gift_card;
        public string name;
        public string variant_inventory_management;
        public List<object> properties;
        public bool product_exists;
        public int fulfillable_quantity;
        public int grams;
        public string price;
        public string total_discount;
        public object fulfillment_status;
        //public PriceSet price_set;
        //public TotalDiscountSet total_discount_set;
        public List<object> discount_allocations;
        public List<object> duties;
        public string admin_graphql_api_id;
        public List<TaxLine> tax_lines;

    }
    public class Fulfillment
    {
        public long id;
        public long order_id;
        public string status;
        public DateTime created_at;
        public string service;
        public DateTime updated_at;
        public object tracking_company;
        public object shipment_status;
        public long location_id;
        public List<LineItem> line_items;
        public object tracking_number;
        public List<object> tracking_numbers;
        public object tracking_url;
        public List<object> tracking_urls;
        //public Receipt receipt;
        public string name;
        public string admin_graphql_api_id;

    }

    public class BillingAddress
    {
        public string first_name;
        public string address1;
        public string phone;
        public string city;
        public string zip;
        public string province;
        public string country;
        public string last_name;
        public string address2;
        public string company;
        public string latitude;
        public string longitude;
        public string name;
        public string country_code;
        public string province_code;

    }
    public class ShippingAddress
    {
        public string first_name;
        public string address1;
        public string phone;
        public string city;
        public string zip;
        public string province;
        public string country;
        public string last_name;
        public string address2;
        public string company;
        public string latitude;
        public string longitude;
        public string name;
        public string country_code;
        public string province_code;

    }

    public class DefaultAddress
    {
        public long id;
        public long customer_id;
        public string first_name;
        public string last_name;
        public string company;
        public string address1;
        public string address2;
        public string city;
        public string province;
        public string country;
        public string zip;
        public string phone;
        public string name;
        public string province_code;
        public string country_code;
        public string country_name;
        //public bool default; 

    }
    public class Customer
    {
        public long id;
        public string email;
        public bool accepts_marketing;
        public DateTime created_at;
        public DateTime updated_at;
        public string first_name;
        public string last_name;
        public int orders_count;
        public string state;
        public string total_spent;
        public long last_order_id;
        public string note;
        public bool verified_email;
        public object multipass_identifier;
        public bool tax_exempt;
        public string phone;
        public string tags;
        public string last_order_name;
        public string currency;
        public DateTime accepts_marketing_updated_at;
        public object marketing_opt_in_level;
        public string admin_graphql_api_id;
        public DefaultAddress default_address;

    }

    public class Refund
    {
        public string id;
        public string order_id;
        public string created_at;
        public string note;
        public string user_id;
        public string processed_at;
        public string restock;
        //public object duties;
        public string admin_graphql_api_id;
        public List<RefundLineItem> refund_line_items;



    }

    public class TaxLine
    {
        public string price;
        public double rate;
        public string title;
    }

    public class RefundLineItem
    {
        public string id;
        public string line_item_id;
        public string quantity;
        public string restock_type;
        public string location_id;
        public string subtotal;
        public string total_tax;
        public LineItem line_item;
    }

    internal class lineItemComparer : IEqualityComparer<LineItem>
    {
        public bool Equals(LineItem x, LineItem y)
        {
            return string.Equals(x.id, y.id) ? true : false;
        }

        public int GetHashCode(LineItem obj)
        {
            return obj.id.GetHashCode();
        }
    }
}