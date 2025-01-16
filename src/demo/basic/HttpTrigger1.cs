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
    public static class HttpTrigger1
    {
        [FunctionName("HttpTrigger1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Order order = JsonConvert.DeserializeObject<Order>(requestBody);

            CosmosDBOperations cosmosDBOperations = new CosmosDBOperations(
                Environment.GetEnvironmentVariable("CosmosDBEndpointUri"),
                Environment.GetEnvironmentVariable("CosmosDBPrimaryKey"),
                Environment.GetEnvironmentVariable("CosmosDBDatabaseId"),
                Environment.GetEnvironmentVariable("CosmosDBContainerId")
            );

            await cosmosDBOperations.SaveOrderAsync(order);

            return new OkObjectResult($"Order received: OrderID = {order.OrderID}, ProductID = {order.ProductID}, Quantity = {order.Quantity}");
        }
    }
}
