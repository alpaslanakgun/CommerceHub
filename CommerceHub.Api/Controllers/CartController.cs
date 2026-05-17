using CommerceHub.Application.DTOs.Cart;
using CommerceHub.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommerceHub.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CartController : BaseApiController
	{
		private readonly ICartService _cartService;

		public CartController(ICartService cartService)
		{
			_cartService = cartService;
		}
		[HttpGet]
		public async Task<IActionResult> GetCart()
		{
			var cart =await _cartService.GetCartAsync(GetUserId());
			return Ok(cart);
		}
		[HttpPost("items")]
		public async Task<IActionResult> AddItem([FromBody] AddToCart dto)
		{
			var cart = await _cartService.AddItemAsync(GetUserId(), dto);
			return Ok(cart,"Urun Sepete Eklendi");
		}
		[HttpPut("items/{id:int}")]
		public async Task<IActionResult> UpdateItem(int id, [FromBody] UpdateCartItemDto updateCartItemDto)
		{
			var cart = await _cartService.UpdateItemAsync(GetUserId(),id,updateCartItemDto);
			return Ok(cart, "Sepet Guncellendi.");
		}
		[HttpDelete("items/{id:int}")]
		public async Task<IActionResult>RemoveItem(int id)
		{
			await _cartService.RemoveItemAsync(GetUserId(),id);
			return Ok<object>(null!,"Ürün sepetten cıkarıldı");
		}
		[HttpDelete("clear")]
		public async Task<IActionResult> Clear()
		{
			await _cartService.ClearCartAsync(GetUserId());
			return Ok<object>(null!, "Sepet temizlendi.");
		}
	}
}
