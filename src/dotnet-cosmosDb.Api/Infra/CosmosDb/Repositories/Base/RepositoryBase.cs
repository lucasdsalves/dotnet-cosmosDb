using dotnet_cosmosDb.Api.Domain.Base;
using dotnet_cosmosDb.Api.Infra.CosmosDb;
using dotnet_cosmosDb.Api.Settings;
using Microsoft.Azure.Cosmos;

namespace dotnet_cosmosDb.Api.Infra.CosmosDb.Repositories.Base
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : Entity
    {
        private readonly Dependencies _dependencies;

        public RepositoryBase(Dependencies dependencies)
        {
            _dependencies = dependencies;
        }

        protected ICosmosDbFactory CosmosDbFactory { get { return _dependencies.CosmosDbFactory; } }

        protected async Task<IList<TEntity>> QueryItems()
        {
            var queryDefinition = new QueryDefinition("SELECT * FROM c");

            FeedIterator<TEntity> queryResultSetIterator =
                CosmosDbFactory
                .GetContainer(typeof(TEntity).Name)
                .GetItemQueryIterator<TEntity>(queryDefinition);

            List<TEntity> entities = new();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<TEntity> currentResultSet = await queryResultSetIterator.ReadNextAsync();

                foreach (TEntity entity in currentResultSet)
                {
                    entities.Add(entity);
                }
            }

            return entities;
        }

        protected async Task<IList<TEntity>> QueryItemsWithParameters(string query, Dictionary<string, object> parameters)
        {
            var queryDefinition = new QueryDefinition(query);

            if (parameters is not null && parameters.Count > 0)
            {
                foreach (KeyValuePair<string, object> parameter in parameters)
                {
                    queryDefinition = queryDefinition.WithParameter(parameter.Key, parameter.Value);
                }
            }

            FeedIterator<TEntity> queryResultSetIterator =
                CosmosDbFactory
                .GetContainer(typeof(TEntity).Name)
                .GetItemQueryIterator<TEntity>(queryDefinition);

            List<TEntity> entities = new();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<TEntity> currentResultSet = await queryResultSetIterator.ReadNextAsync();

                foreach (TEntity entity in currentResultSet)
                {
                    entities.Add(entity);
                }
            }

            return entities;
        }

        protected async Task InsertItem(TEntity item, string partitionKey)
        {
            ItemResponse<TEntity> response =
                await CosmosDbFactory
                .GetContainer(typeof(TEntity).Name)
                .CreateItemAsync(item, new PartitionKey(partitionKey));
        }

        protected async Task UpdateItem(TEntity item, string partitionKey)
        {
            await CosmosDbFactory
              .GetContainer(typeof(TEntity).Name)
              .ReplaceItemAsync(item, item.Id, new PartitionKey(partitionKey));
        }

        protected async Task DeleteItem(string itemId, string partitionKey)
        {
            await CosmosDbFactory
            .GetContainer(typeof(TEntity).Name)
            .DeleteItemAsync<TEntity>(itemId, new PartitionKey(partitionKey));
        }
    }
}
