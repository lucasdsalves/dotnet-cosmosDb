using ProductEntity = dotnet_cosmosDb.Api.Domain.Product.Entities.Product;
using dotnet_cosmosDb.Api.Infra.CosmosDb.Repositories.Base;
using dotnet_cosmosDb.Api.Domain.Product.Repository;
using dotnet_cosmosDb.Api.Settings;

namespace dotnet_cosmosDb.Api.Infra.CosmosDb.Repositories.Product
{
    public class ProductRepository : RepositoryBase<ProductEntity>, IProductRepository
    {
        public ProductRepository(Dependencies dependencies) : base(dependencies)
        {
        }

        public async Task Add(ProductEntity product)
        {
            await InsertItem(product, product.Name);
        }

        public async Task Delete(string productId)
        {
            await DeleteItem(productId, "Name");
        }

        public async Task Edit(ProductEntity product)
        {
            await UpdateItem(product, product.Name);
        }

        public async Task<IList<ProductEntity>> List()
        {
            return await QueryItems();
        }

        public async Task<IList<ProductEntity>> ListById(string productId)
        {
            string query =
                $@"
                    SELECT 
                        *  
                    FROM 
                        {nameof(Product)} product
                    WHERE
                        product.{nameof(ProductEntity.Id)} = @productId
                ";

            Dictionary<string, object> parameters = new()
            {
                { "@productId", productId }
            };

            IList<ProductEntity> products = await QueryItemsWithParameters(query, parameters);

            return products;
        }
    }
}
