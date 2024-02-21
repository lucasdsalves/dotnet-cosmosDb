using dotnet_cosmosDb.Api.Infra.CosmosDb;

namespace dotnet_cosmosDb.Api.Settings.CosmosDb
{
    public class CosmosDbStartup
    {
        private readonly ICosmosDbFactory _cosmosDbFactory;

        public CosmosDbStartup(ICosmosDbFactory cosmosDbFactory)
        {
            _cosmosDbFactory = cosmosDbFactory;
        }

        public async Task StartupCosmosDb()
        {
            await _cosmosDbFactory.CreateDbContainers();
        }
    }
}
