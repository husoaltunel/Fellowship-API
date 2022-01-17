using Business.Managers.Users.Commands.DeleteUser;
using Business.Managers.Users.Commands.UpdateUser;
using Business.Managers.Users.Queries.GetUserById;
using Business.Managers.Users.Queries.GetUserByUserName;
using Business.Managers.Users.Queries.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator ;
        public UsersController(IMediator mediator)
        {
            
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersAsync()
        {
            
            return Ok(await _mediator.Send(new GetUsersQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync(int id)
        {
            return Ok(await _mediator.Send(new GetUserByIdQuery() { Id = id }));
        }

        [HttpGet("get-user-by-username/{username}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            return Ok(await _mediator.Send(new GetUserByUserNameQuery(){Username = username}));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserAsync(UpdateUserCommand model)
        {
            return Ok(await _mediator.Send(model));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            return Ok(await _mediator.Send(new DeleteUserCommand(){ Id = id }));
        }
        
    }
}
