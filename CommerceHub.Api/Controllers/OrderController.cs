using CommerceHub.Application.DTOs.Order;
using CommerceHub.Application.Interfaces;
using CommerceHub.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommerceHub.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : BaseApiController
	{

		//Satislar 

		private readonly IOrderService _orderService;

		public OrderController(IOrderService orderService)
		{
			_orderService = orderService;
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateOrderDto createOrderDto)
		{
			var order = await _orderService.CreateOrderAsync(GetUserId(),createOrderDto);
			return Created(order, "Sipariş Olusturuldu");
		}

		[HttpGet("my-orders")]
		public async Task<IActionResult> GetMyOrders()
		{
			var orders= await _orderService.GetMyOrdersAsync(GetUserId());
			return Ok(orders);
		}

		[HttpGet("{id:int}")]
		public async Task<IActionResult>GetById(int id)
		{
			var order = await _orderService.GetOrderByIdAsync(id,GetUserId(),IsAdmin());
			return Ok(order);
		}
		[Authorize(Roles =UserRoles.Admin)]
		[HttpGet("admin")]
		public async Task<IActionResult> GetAll()
		{
			var orders= await _orderService.GetAllOrdersAsync();
			return Ok(orders);
		}
		[Authorize(Roles = UserRoles.Admin)]
		[HttpGet("{id:int}/status")]
		public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateOrderStatusDto updateStatusDto)
		{
			await _orderService.UpdateOrderStatusAsync(id, updateStatusDto.NewStatus);
			return Ok(updateStatusDto,"Sipariş Durumu Güncellendi");
		}
	}
}
