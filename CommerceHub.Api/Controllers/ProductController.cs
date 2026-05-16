using CommerceHub.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommerceHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? categoryId)
        {
            var products = await _productService.GetAllAsync(categoryId);
            return Ok(products);



        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult>GetById(int id)
        {
            var product= await _productService.GetByIdAsync(id);
            return Ok(product);
        }

    }
}
