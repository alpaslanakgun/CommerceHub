using CommerceHub.Application.DTOs.Product;
using CommerceHub.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommerceHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseApiController
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
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateDto productCreateDto)
        {
            var product = await _productService.CreateAsync(productCreateDto);
            return Created(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,ProductUpdateDto productUpdateDto)
        {
            var product= await _productService.UpdateAsync(id, productUpdateDto);
            return Ok(product, "Urun Guncellendi");
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);
            return Ok<object>(null, "Urun Silindi");
        }

		/*
         
         Serilog entegrasyonu ,
         Inmemory cache ve distributed cache nedir arasındaki farklar nedir bu yaptıgımız projede 
         uygulanır mı uygulanır ise nereye uygularız ve nasıl uygularız bunla isteyenler makale yazıp linkedin yada medium ' a paylasabilirsiniz. 

        --Docker nedir ne işe yarar bunla alakalı araştırma yapalım 

        --RabbitMQ nedir ne işe yarar? bu projede uygularasak nasıl uygularız ve dead letter exchange uygulamak istesek nereye neden uygularız ? 

        https://www.youtube.com/watch?v=jmdrp0rCCCA&list=PLQVXoXFVVtp3PPhdV_uk4P6KrSWqUEOAW 
         desing patternlar 
         */

	}
}
