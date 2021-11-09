using Identity.Service.Queries;
using Identity.Service.Queries.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Common.Collection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserQueryService _userQueryService;
        private readonly ILogger<UserController> _logger;
        private readonly IMediator _mediator;

        public UserController(
            IUserQueryService userQueryService,
            ILogger<UserController> logger, 
            IMediator mediator)
        {
            _userQueryService = userQueryService;
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<DataCollection<UserDto>> GetAll(int page = 1, int take = 10, string ids = null)
        {
            IEnumerable<string> users = ids?.Split(',');
            return await _userQueryService.GetAllAsync(page, take, users);
        }

        [HttpGet("{id}")]
        public async Task<UserDto> GetById(string id)
        {
            return await _userQueryService.GetByIdAsync(id);
        }
    }
}
