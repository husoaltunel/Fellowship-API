using Business.Utilities.Results.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Managers.Photos.Commands.SetProfilePhoto
{
    public class SetProfilePhotoCommand : IRequest<IResult>
    {
        public int UserId { get;set;}
        public int PhotoId { get;set;}
    }
}
