using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace Company.Function
{
    public class CosmosDBHelper
    {
        private CosmosClient _cosmosClient;
        private Database _database;
        private Container _container;

        public CosmosDBHelper(string connectionString, string databaseName, string containerName)
        {
            _cosmosClient = new CosmosClient(connectionString);
            _database = _cosmosClient.GetDatabase(databaseName);
            _container = _database.GetContainer(containerName);
        }

        public async Task SaveOrderAsync(Order order)
        {
            await _container.CreateItemAsync(order, new PartitionKey(order.OrderID));
        }
    }
}
