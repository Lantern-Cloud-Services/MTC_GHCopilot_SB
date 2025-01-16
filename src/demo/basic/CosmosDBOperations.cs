using System;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace Company.Function
{
    public class CosmosDBOperations
    {
        private CosmosClient _cosmosClient;
        private Database _database;
        private Container _container;

        public CosmosDBOperations(string endpointUri, string primaryKey, string databaseId, string containerId)
        {
            _cosmosClient = new CosmosClient(endpointUri, primaryKey);
            _database = _cosmosClient.GetDatabase(databaseId);
            _container = _database.GetContainer(containerId);
        }

        public async Task SaveOrderAsync(Order order)
        {
            try
            {
                await _container.CreateItemAsync(order, new PartitionKey(order.OrderID));
            }
            catch (CosmosException ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                throw;
            }
        }
    }
}
