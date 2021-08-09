using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Managers.Account.Commands.Add;
using Business.Managers.Account.Queries.GetAll;
using Business.Managers.Account.Queries.GetById;
using Business.Managers.Account.Queries.Login;
using Entities.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserAddCommand model)
        {
            return Ok(await _mediator.Send(model));

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginQuery model)
        {
            return Ok(await _mediator.Send(model));
        }
        [HttpGet("get-users")]
        public async Task<IActionResult> GetUsersAsync()
        {
            return Ok(await _mediator.Send(new UserGetAllQuery()));
        }
        
        [Authorize]
        [HttpGet("get-user")]
        public async Task<IActionResult> GetUserAsync(int id)
        {
            return Ok(await _mediator.Send(new UserGetByIdQuery(){ Id = id }));
        }
    }
}
