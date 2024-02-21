using dotnet_cosmosDb.Api.Domain.Product.Entities;
using dotnet_cosmosDb.Api.Domain.Product.Repository;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_cosmosDb.Api.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productsRepository;

        public ProductsController(IProductRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ListProducts()
        {
            return new JsonResult(await _productsRepository.List());
        }

        [HttpPost]
        public async Task<IActionResult> NewProduct()
        {
            var product = new Product(
                name: "teste",
                description: "description",
                price: 10,
                stockQuantity: 300
                );

            await _productsRepository.Add(product);

            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> ChangeProduct(string productId)
        {
            var product = await _productsRepository.ListById(productId);

            await _productsRepository.Edit(product.First());

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(string productId)
        {
            await _productsRepository.Delete(productId);

            return Ok();
        }
    }
}
