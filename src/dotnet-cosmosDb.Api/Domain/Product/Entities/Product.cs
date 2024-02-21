using dotnet_cosmosDb.Api.Domain.Base;

namespace dotnet_cosmosDb.Api.Domain.Product.Entities
{
    public class Product : Entity
    {
        public Product(string name, string description, double price, int stockQuantity)
        {
            Name = name;
            Description = description;
            Price = price;
            StockQuantity = stockQuantity;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int StockQuantity { get; set; }
    }
}
