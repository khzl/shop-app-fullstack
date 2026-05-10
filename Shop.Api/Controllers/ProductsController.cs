using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interfaces;

namespace Shop.Api.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        // private properties 
        private readonly IProductService _productService;

        // public Constructor
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // Feature 4 - > Get All Products
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        // Feature 5 -> Search And Pagination
        [HttpGet("search")]
        public async Task<IActionResult> Search(
            string searchTerm,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var result = await _productService.SearchProductsAsync(
                searchTerm, pageNumber, pageSize);
            return Ok(result);
        }


    }
}
