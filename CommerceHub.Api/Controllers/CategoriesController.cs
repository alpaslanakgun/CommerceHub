using CommerceHub.Application.DTOs.Category;
using CommerceHub.Application.DTOs.Product;
using CommerceHub.Application.Interfaces;
using CommerceHub.Domain.Entities;
using CommerceHub.Persistence.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommerceHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseApiController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _categoryService.GetAllAsync();
            return Ok(products);

        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _categoryService.GetByIdAsync(id);
            return Ok(product);
        }
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CategoryCreateDto categoryCreateDto)
		{
			var category = await _categoryService.CreateAsync(categoryCreateDto);
			return Created(category);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, CategoryUpdateDto  categoryUpdateDto)
		{
			var category = await _categoryService.UpdateAsync(id, categoryUpdateDto);
			return Ok(category, "Kategori Guncellendi");
		}


		[HttpDelete]
		public async Task<IActionResult> Delete(int id)
		{
			await _categoryService.DeleteAsync(id);
			return Ok<object>(null, "Kategori Silindi");
		}

	}
}
