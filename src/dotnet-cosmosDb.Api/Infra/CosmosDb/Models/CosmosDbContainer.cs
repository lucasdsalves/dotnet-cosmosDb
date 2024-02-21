namespace dotnet_cosmosDb.Api.Infra.CosmosDb.Models
{
    public class CosmosDbContainer
    {
        public CosmosDbContainer(
            string id,
            string partitionKey)
        {
            Id = id;
            PartitionKey = partitionKey;
        }

        public string Id { get; set; }
        public string PartitionKey { get; set; }
    }
}
