using Business.Managers.UserLikes.Commands.DeleteUserLike;
using Business.Managers.UserLikes.Commands.InsertUserLike;
using Business.Managers.UserLikes.Queries.DoILikeTheUser;
using Business.Managers.UserLikes.Queries.GetUserLikesByUsername;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserLikesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly AuthController _authController;
        public UserLikesController(IMediator mediator,AuthController authController)
        {
            _mediator = mediator;
            _authController = authController;
        }

        [HttpPost("{acclaimedUserId}")]
        public async Task<IActionResult> InsertUserLikeAsync(int acclaimedUserId)
        {
            return Ok(await _mediator.Send(new InsertUserLikeCommand(){ AcclaimedUserId = acclaimedUserId, UserLikedId = int.Parse(_authController.GetUserIdFromToken())  }));
        }

        [HttpGet]
        public async Task<IActionResult> GetUserLikesAsync()
        {
            return Ok(await _mediator.Send(new GetUserLikesByUsernameQuery(){ UserId = int.Parse(_authController.GetUserIdFromToken()) }));
        }

        [HttpGet("{acclaimedUserId}")]
        public async Task<IActionResult> DoILikeTheUser(int acclaimedUserId)
        {
            return Ok(await _mediator.Send(new DoILikeTheUserQuery(){ UserId = int.Parse(_authController.GetUserIdFromToken()) , AcclaimedUserId = acclaimedUserId }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserLikeAsync(int id)
        {
            return Ok(await _mediator.Send(new DeleteUserLikeCommand(){ UserLikedId = int.Parse(_authController.GetUserIdFromToken()) , AcclaimedUserId = id }));
        }

    }
}
