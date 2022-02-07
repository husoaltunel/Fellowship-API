using Business.Utilities.Results.Abstract;
using Entities.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Managers.UserLikes.Commands.DeleteUserLike
{
    public class DeleteUserLikeCommand : DeleteUserLikeDto, IRequest<IResult>
    {

    }
}
