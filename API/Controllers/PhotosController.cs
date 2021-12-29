using Business.Managers.Photo.Commands.InsertPhoto;
using Business.Managers.Photo.Queries.GetPhotosByUsername;
using Business.Managers.Photo.Queries.GetProfilePhotoByUsername;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PhotosController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly AuthController _authController;
        public PhotosController(IMediator mediator, AuthController authController)
        {
            _mediator = mediator;
            _authController = authController;
        }
       
        [HttpGet("get-all-by-username/{username}")]
        public async Task<IActionResult> GetAllByUsernameAsync(string username)
        {
            return Ok(await _mediator.Send(new GetPhotosByUsernameQuery(){ Username = username }));
        }

        [HttpGet("get-profile-photo-by-username/{username}")]
        public async Task<IActionResult> GetProfilePhotoByUsernameAsync(string username)
        {
            return Ok(await _mediator.Send(new GetProfilePhotoByUsernameQuery(){ Username = username }));
        }

        [HttpPost,DisableRequestSizeLimit]
        public async Task<IActionResult> UploadAsync(List<IFormFile> files)
        {
            return Ok(await _mediator.Send(new InsertUserPhotosCommand(){ UserId = _authController.GetUserIdFromToken(), PhotoFiles = files  }));
        }

    }
}
