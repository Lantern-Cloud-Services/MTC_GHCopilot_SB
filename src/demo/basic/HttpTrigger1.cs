using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;


/*

The provided code defines an Azure Function named HttpTrigger1 within the Company.Function namespace. This function is designed to handle HTTP requests and is marked with the [FunctionName("HttpTrigger1")] attribute, which specifies the name of the function. The Run method is the entry point for the function and is defined as public static async Task<IActionResult> Run. It takes two parameters: an HttpRequest object named req and an ILogger object named log.

The function supports both GET and POST HTTP methods, as indicated by the [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] attribute. This attribute also specifies that the function requires function-level authorization.

Inside the Run method, the function logs a message indicating that it has processed a request using the log.LogInformation method. It then attempts to retrieve a query parameter named "name" from the request URL and assigns it to the name variable.

Next, the function reads the entire body of the HTTP request asynchronously using a StreamReader and the ReadToEndAsync method. The request body is then deserialized into a dynamic object using JsonConvert.DeserializeObject. This allows the function to access properties of the JSON payload dynamically. The function extracts the productID, quantity, and orderID properties from the deserialized object, providing a default value of 0 for quantity if it is not present in the request.

Finally, the function returns an OkObjectResult with a response string, indicating that the request was processed successfully. This response will be sent back to the client that made the HTTP request.

*/
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