using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;



namespace Company.Function
{
    /// <summary>
    /// Azure Function that processes HTTP GET and POST requests.
    /// </summary>
    public static class HttpTrigger1
    {
        /// <summary>
        /// Runs the HTTP trigger function.
        /// </summary>
        /// <param name="req">The HTTP request.</param>
        /// <param name="log">The logger.</param>
        /// <returns>An IActionResult containing the response.</returns>
        [FunctionName("HttpTrigger1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            log.LogInformation("C# HTTP trigger function processed a request.");

            // Get the 'name' query parameter from the request URL
            string name = req.Query["name"];

            // Read the request body
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            // Extract values from the request body
            int productID = data?.productID;
            int quantity = data?.quantity ?? 0; // Default to 0 if quantity is not present
            int orderID = data?.orderID;

            // Return a response
            return new OkObjectResult($"Hello, {name}. This HTTP triggered function executed successfully. Product ID: {productID}, Quantity: {quantity}, Order ID: {orderID}");

        }
    }
}