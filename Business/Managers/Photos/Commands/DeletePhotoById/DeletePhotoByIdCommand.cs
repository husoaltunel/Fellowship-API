using Business.Utilities.Results.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Managers.Photos.Commands.DeletePhotoById
{
    public class DeletePhotoByIdCommand : IRequest<IResult>
    {
        public int Id { get;set;}
        public string UserId { get;set;}
    }
}
