using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopifyBridge.Models
{
    public class OrderModel
    {
        public string WarehouseName { get; set; }
        public DateTime PickupDueDate { get; set; }
        public int Package_Serial { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public double? Total_Weight { get; set; }
        public string Service_Type { get; set; }
        public string Service { get; set; }
        public DateTime? ServiceDate { get; set; }
        public string Service_Category { get; set; }
        public string Payment_Type { get; set; }
        public double? COD_Value { get; set; }
        public List<PieceDTO> Pieces { get; set; }
        public string Special_Notes { get; set; }
        public string Customer_Name { get; set; }
        public string Mobile_No { get; set; }
        //public int? Building_No { get; set; }
        public string Street { get; set; }
        //public int? Floor_No { get; set; }
        //public int? Apartment_No { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Neighborhood { get; set; }
        //public string District { get; set; }
        //public string GeoLocaion { get; set; }
        public string Address_Category { get; set; }
        //public string Telephone { get; set; }
        //public string Address2 { get; set; }
        //public string CustVal { get; set; }
        public string Currency { get; set; }
    }
    public enum Service_Type
    {
        DTD = 0,
        DTC = 1,
        CTD = 2,
        CTC = 3
    }
    public enum Service
    {
        SD = 0,
        ND = 1
    }
    public enum Service_Category
    {
        DELIVERY = 0,
        RETURN = 1
    }
    public enum Payment_Type
    {
        PP = 0,
        COD = 1,
        CC = 3
    }
    public enum Product_Category
    {
        food = 0,
        Phones = 1,
        Elec = 2,
        Health = 3,
        Fashion = 4,
        Comp = 5,
        Baby = 6,
        Furn = 7
    }
    public enum Address_Category
    {
        H = 0,
        OF = 1
    }
    public class PieceDTO
    {
        public PieceDTO(int pieceNo, string SpecialNotes)
        {
            this.pieceNo = pieceNo;
            this.SpecialNotes = SpecialNotes;
        }

        public int pieceNo { get; set; }
        public int Weight { get; set; }
        public Product_Category Item_Category { get; set; }
        public string SpecialNotes { get; set; }
        public string Dimentions { get; set; }

    }
    public class RequestPackage
    {
        public List<LineItem> LineItems { get; set; }

        public string Warehouse { get; set; }


    }
}