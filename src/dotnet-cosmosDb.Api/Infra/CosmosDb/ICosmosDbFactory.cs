using Microsoft.Azure.Cosmos;

namespace dotnet_cosmosDb.Api.Infra.CosmosDb
{
    public interface ICosmosDbFactory
    {
        Task CreateDbContainers();
        Container GetContainer(string containerId);
    }
}
