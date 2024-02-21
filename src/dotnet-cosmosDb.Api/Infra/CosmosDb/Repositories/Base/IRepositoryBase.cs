using dotnet_cosmosDb.Api.Domain.Base;

namespace dotnet_cosmosDb.Api.Infra.CosmosDb.Repositories.Base
{
    public interface IRepositoryBase<TEntity> where TEntity : Entity
    {
    }
}
