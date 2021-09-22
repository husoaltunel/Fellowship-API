using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Managers.Account.Commands.Add;
using Business.Managers.Account.Commands.Delete;
using Business.Managers.Account.Commands.Update;
using Business.Managers.Account.Queries.GetAll;
using Business.Managers.Account.Queries.GetById;
using Entities.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-users")]
        public async Task<IActionResult> GetUsersAsync()
        {
            return Ok(await _mediator.Send(new UserGetAllQuery()));
        }
        [HttpGet("get-user")]
        public async Task<IActionResult> GetUserAsync(int id)
        {
            return Ok(await _mediator.Send(new UserGetByIdQuery(){  Id = id}));
        }

        [HttpPost("add-user")]
        public async Task<IActionResult> AddUserAsync(UserAddCommand model)
        {

            return Ok(await _mediator.Send(model));
        }
        [HttpPost("update-user")]
        public async Task<IActionResult> UpdateUserAsync(UserUpdateCommand model)
        {
            return Ok(await _mediator.Send(model));
        }
        [HttpPost("delete-user")]
        public async Task<IActionResult> DeleteUserAsync(UserDeleteCommand model)
        {
            return Ok(await _mediator.Send(model));
        }
    }
}
