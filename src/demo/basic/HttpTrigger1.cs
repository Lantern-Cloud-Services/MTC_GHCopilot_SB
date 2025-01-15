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

            string connectionString = Environment.GetEnvironmentVariable("CosmosDBConnectionString");
            string databaseName = Environment.GetEnvironmentVariable("CosmosDBDatabaseName");
            string containerName = Environment.GetEnvironmentVariable("CosmosDBContainerName");

            CosmosDBHelper cosmosDBHelper = new CosmosDBHelper(connectionString, databaseName, containerName);
            await cosmosDBHelper.SaveOrderAsync(order);

            return new OkObjectResult($"Order received. Order ID: {order.OrderID}, Product ID: {order.ProductID}, Quantity: {order.Quantity}");
        }
    }
}
