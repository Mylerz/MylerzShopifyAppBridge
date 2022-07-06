using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopifyBridge.Models
{
    public class APIResponse
    {
        public APIResponse(APIResponseData value, bool isErrorState, string errorDescription, string errorStackTrace)
        {
            Value = value;
            IsErrorState = isErrorState;
            ErrorDescription = errorDescription;
            ErrorStackTrace = errorStackTrace;
        }

        public APIResponseData Value { get; set; }
        public bool IsErrorState { get; set; }
        public string ErrorDescription { get; set; }
        public string ErrorStackTrace { get; set; }
    }

    public class APIResponseData
    {
        public APIResponseData(List<(LineItem, long?)> barcodePerLineItem)
        {
            BarcodePerLineItem = barcodePerLineItem;
        }

        public List<(LineItem, long?)> BarcodePerLineItem { get ; set; }
    }
}