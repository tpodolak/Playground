using System;
using System.Collections.Generic;
using DotRezCore.Api.Models.V1;
using DotRezCore.Api.OperationFilters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DotRezCore.Api.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly ILogger<OrdersController> _ordersController;

        public OrdersController(ILogger<OrdersController> ordersController)
        {
            _ordersController = ordersController;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Order>), 200)]
        [SwaggerResponseHeader(Name = Constants.Session.XSessionTokenHeaderName, Description = "", Type = "", StatusCode = 200)]
        public IActionResult Get([FromQuery] GetOrderRequest request)
        {
            var orders = new[]
            {
                new Order {Id = 1, Customer = "John Doe"},
                new Order {Id = 2, Customer = "Bob Smith"},
                new Order {Id = 3, Customer = "Jane Doe", EffectiveDate = DateTimeOffset.UtcNow.AddDays(7d)}
            };

            return Ok(orders);
        }

        [HttpGet]
        [ProducesResponseType(typeof(Dictionary<string, Order>), 200)]
        [Route("GetSomethingElse")]
        public IActionResult GetSomethingElse(int id, int id2, DateTime id3)
        {
            var orders = new[]
            {
                new Order {Id = 1, Customer = "John Doe"},
                new Order {Id = 2, Customer = "Bob Smith"},
                new Order {Id = 3, Customer = "Jane Doe", EffectiveDate = DateTimeOffset.UtcNow.AddDays(7d)}
            };

            return Ok(orders);
        }
        
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Order), 200)]
        [ProducesResponseType(typeof(object), 400)]
        [ProducesResponseType(typeof(object), 404)]
        public IActionResult Get(int id) => Ok(new Order {Id = id, Customer = "John Doe"});

        [HttpPost]
        [ProducesResponseType(typeof(Order), 201)]
        [ProducesResponseType(typeof(object), 400)]
        public IActionResult Post([FromBody] Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            order.Id = 42;

            return CreatedAtRoute(new {id = order.Id}, order);
        }
    }
}