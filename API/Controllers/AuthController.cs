using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Business.Managers.Auth.Commands.Register;
using Business.Managers.Auth.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCommand model)
        {
            return Ok(await _mediator.Send(model));

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginQuery model)
        {
            return Ok(await _mediator.Send(model));
        }

        [Authorize]
        [HttpGet("is-logged-in")]
        public IActionResult IsLoggedIn()
        {
            return Ok();
        }

        [Authorize]
        [ApiExplorerSettings(IgnoreApi = true)]
        public string GetUserIdFromToken()
        {
            return _httpContextAccessor.HttpContext.User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;

        }


    }
}
