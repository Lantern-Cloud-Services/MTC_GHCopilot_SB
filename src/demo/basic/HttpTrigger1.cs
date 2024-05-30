using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Cosmos;

namespace Company.Function
{
    public static class HttpTrigger1
    {
        private static CosmosClient cosmosClient;
        private static Container cosmosContainer;

        static HttpTrigger1()
        {
            cosmosClient = new CosmosClient(Environment.GetEnvironmentVariable("CosmosDBConnectionString"));
            cosmosContainer = cosmosClient.GetContainer("OrderDatabase", "OrderContainer");
        }

        [FunctionName("HttpTrigger1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Order order = JsonConvert.DeserializeObject<Order>(requestBody);

            try
            {
                ItemResponse<Order> response = await cosmosContainer.CreateItemAsync(order, new PartitionKey(order.OrderID));
                return new OkObjectResult($"Order with ID: {order.OrderID} added to Cosmos DB.");
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                return new ConflictObjectResult($"Order with ID: {order.OrderID} already exists.");
            }
        }
    }

    public class Order
    {
        [JsonProperty("id")]
        public string OrderID { get; set; }
        public string ProductID { get; set; }
        public int Quantity { get; set; }
    }
}
