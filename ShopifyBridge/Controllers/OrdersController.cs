using ShopifyBridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Configuration;

namespace ShopifyBridge.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class OrdersController : ApiController
    {
       

        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        static string mylerzIntegrationApiLink = ConfigurationManager.AppSettings["IntegrationApiLink"];

        string url = $"{mylerzIntegrationApiLink}/api/orders/addorders";



        // POST: api/Orders
        public async Task<IHttpActionResult> Post([FromBody]Requst request)
        {

            try
            {

                APIResponse response = await Helper.FulfillOrders(request.orders, request.token, url,logger);

                return Ok(response);
            }
            catch (Exception exc)
            {
                return Ok(new APIResponse(null, true, $"Undefined Error Occured: {exc.Message}", exc.StackTrace));

            }

        }


    }
}
