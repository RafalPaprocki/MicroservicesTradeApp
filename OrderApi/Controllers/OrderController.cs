using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrderService.Common;
using OrderService.Dto;
using OrderService.Infrastructure;
using OrderService.IntegrationsEvent;
using OrderService.Model;
using OrderService.Saga.CreateOrderSaga;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<OrderController> _logger;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly OrderContext _context;
        private readonly IMapper _mapper;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly CreateOrderSagaDbContext _sagaContext;
        
        public OrderController(ILogger<OrderController> logger, IPublishEndpoint publishEndpoint, OrderContext context, IMapper mapper, ISendEndpointProvider sendEndpointProvider, CreateOrderSagaDbContext sagaContext)
        {
            _publishEndpoint = publishEndpoint;
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _sendEndpointProvider = sendEndpointProvider;
            _sagaContext = sagaContext;
        }

        [HttpPost]
        public async Task<ActionResult> Post(OrderCreateDto orderDto)
        {
            try
            {
                // TODO remove
                var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:orderCreated")); 
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                Order order = _mapper.Map<Order>(orderDto);
                order.UserId = 56748; // TODO: get user id from request token
                order.Status = Status.Pending;
                _context.Add(order);
                await _context.SaveChangesAsync();

                var orderSubmittedEvent = _mapper.Map<OrderSubmitted>(order);
                await _publishEndpoint.Publish(orderSubmittedEvent);
                await endpoint.Send(orderSubmittedEvent); // TODO remove
                
                return CreatedAtAction(nameof(Get), new {id = order.Id}, order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var userId = 56748; // TODO Get user id from request token
            var orders = await _context.Orders
                .Where(x => x.UserId == userId)
                .ToListAsync();

            var ordersDto = _mapper.Map<List<Order>, List<OrderDto>>(orders);
            return Ok(ordersDto);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var userId = 56748; // TODO Get user id from request token
            var order = await _context.Orders
                .Where(x => x.UserId == userId)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            if (order == null)
            {
                return NotFound($"Order with id {id} is not exists for given user");
            }

            var orderDto = _mapper.Map<OrderDto>(order);
            return Ok(orderDto);
        }
    }
}