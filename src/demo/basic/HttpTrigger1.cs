using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;



/// <summary>
/// Azure Function that processes HTTP GET and POST requests.
/// </summary>
/// <param name="req">The HTTP request object.</param>
/// <param name="log">The logger object to log information.</param>
/// <returns>An IActionResult containing a greeting message and details from the request body.</returns>
/// <remarks>
/// This function expects a 'name' query parameter in the URL and a JSON body with fields 'productID', 'quantity', and 'orderID'.
/// </remarks>
namespace Company.Function
{
    public static class HttpTrigger1
    {
        [FunctionName("HttpTrigger1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            log.LogInformation("C# HTTP trigger function processed a request.");

            // get the 'name' query parameter from the request URL
            string name = req.Query["name"];

            // read the request body into a string
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            // parse the request body into a JSON object with fields productID and quantity and orderID
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            int productID = data?.productID;
            int quantity = data?.quantity;
            int orderID = data?.orderID;



            return new OkObjectResult($"Hello, {name}. This HTTP triggered function executed successfully. Product ID: {productID}, Quantity: {quantity}, Order ID: {orderID}");

        }
    }
}