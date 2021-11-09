using Catalog.Service.EventHandlers.Commands;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Catalog.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("stocks")]
    public class ProductInStockController : ControllerBase
    {
        private readonly ILogger<ProductInStockController> _logger;
        private readonly IMediator _mediator;
        public ProductInStockController(
            ILogger<ProductInStockController> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStock(ProductInStockUpdateStockCommand command)
        {
            await _mediator.Publish(command);
            return NoContent();
        }
    }
}
