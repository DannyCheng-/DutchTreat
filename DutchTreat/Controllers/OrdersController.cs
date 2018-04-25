using DutchTreat.Data;
using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
	[Route("api/[controller]")]
	public class OrdersController : Controller
	{
		private readonly IDutchRepository _repository;
		private readonly ILogger<OrdersController> _logger;

		public OrdersController(IDutchRepository repository, ILogger<OrdersController> logger)
		{
			_repository = repository;
			_logger = logger;
		}

		[HttpGet]
		public IActionResult Get()
		{
			try
			{
				return Ok(_repository.GetAllOrders());
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get orders: {ex}");
				return BadRequest("Failed to get orders");
			}
		}

		[HttpGet("{id:int}")]
		public IActionResult Get(int id)
		{
			try
			{
				var order = _repository.GetOrderById(id);
				if (order != null) return Ok(order);
				else return NotFound();
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get order: {ex}");
				return BadRequest("Failed to get order");
			}
		}

		[HttpPost]
		public IActionResult Post([FromBody]Order model)
		{
			try
			{
				_repository.AddEntity(model);
				if (_repository.SaveAll())
				{
					return Created($"/api/orders/{model.Id}", model);
				}

				return BadRequest("Failed to create order");
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to create order: {ex}");
				return BadRequest("Failed to create order");
			}
		}
    }
}
