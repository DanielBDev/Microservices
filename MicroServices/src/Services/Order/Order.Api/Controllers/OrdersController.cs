using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Order.Service.EventHandlers.Commands;
using Order.Service.Queries;
using Order.Service.Queries.DTOs;
using Service.Common.Collection;
using System.Threading.Tasks;

namespace Order.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderQueryService _orderQueryService;
        private readonly ILogger<OrdersController> _logger;
        private readonly IMediator _mediator;

        public OrdersController(
            IOrderQueryService orderQueryService,
            ILogger<OrdersController> logger,
            IMediator mediator)
        {
            _orderQueryService = orderQueryService;
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<DataCollection<OrderDto>> GetAll(int page = 1, int take = 10, string ids = null)
        {
            return await _orderQueryService.GetAllAsync(page, take);
        }

        [HttpGet("{id}")]
        public async Task<OrderDto> GetById(int id)
        {
            return await _orderQueryService.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderCreateCommand notification)
        {
            await _mediator.Publish(notification);
            return Ok();
        }
    }
}
