using dotnet_cosmosDb.Api.Infra.CosmosDb;

namespace dotnet_cosmosDb.Api.Settings
{
    public class Dependencies
    {
        public Dependencies(ICosmosDbFactory cosmosDbFactory)
        {
            CosmosDbFactory = cosmosDbFactory;
        }

        public ICosmosDbFactory CosmosDbFactory { get; private set; }
    }
}
