using ProductEntity = dotnet_cosmosDb.Api.Domain.Product.Entities.Product;

namespace dotnet_cosmosDb.Api.Domain.Product.Repository
{
    public interface IProductRepository
    {
        Task<IList<ProductEntity>> List();
        Task<IList<ProductEntity>> ListById(string productId);
        Task Add(ProductEntity product);
        Task Edit(ProductEntity product);
        Task Delete(string productId);
    }
}
