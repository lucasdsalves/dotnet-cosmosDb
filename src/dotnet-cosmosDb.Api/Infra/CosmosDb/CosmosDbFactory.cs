using dotnet_cosmosDb.Api.Domain.Product.Entities;
using dotnet_cosmosDb.Api.Infra.CosmosDb.Models;
using dotnet_cosmosDb.Api.Settings.CosmosDb;
using Microsoft.Extensions.Options;
using Microsoft.Azure.Cosmos;

namespace dotnet_cosmosDb.Api.Infra.CosmosDb
{
    public class CosmosDbFactory : ICosmosDbFactory
    {
        private readonly string _endpointUri;
        private readonly string _primaryKey;
        private readonly string _databaseId = "db";

        private CosmosClient cosmosClient;
        private Database database;
        private Container container;

        public CosmosDbFactory(IOptions<CosmosDbSettings> cosmosDbSettings)
        {
            _endpointUri = cosmosDbSettings.Value.Endpoint;
            _primaryKey = cosmosDbSettings.Value.PrimaryKey;

            cosmosClient = new CosmosClient(
                           _endpointUri,
                           _primaryKey,
                           new CosmosClientOptions()
                           {
                               ApplicationName = "CosmosDbPOC"
                           });
        }

        public Container GetContainer(string containerId)
        {
            return cosmosClient.GetContainer(_databaseId, containerId);
        }

        public async Task CreateDbContainers()
        {
            await CreateDatabase();

            var containers = ListContainers();

            foreach (var container in containers)
            {
                await CreateContainer(container.Id, container.PartitionKey);
            }
        }

        private async Task CreateDatabase()
        {
            database = await cosmosClient.CreateDatabaseIfNotExistsAsync(_databaseId);
        }

        private async Task CreateContainer(string containerId, string partitionKey)
        {
            container = await database.CreateContainerIfNotExistsAsync(containerId, partitionKey, 400);
        }

        private List<CosmosDbContainer> ListContainers()
        {
            return new List<CosmosDbContainer>()
            {
                new CosmosDbContainer(nameof(Product), "/Name"),
            };
        }
    }
}
