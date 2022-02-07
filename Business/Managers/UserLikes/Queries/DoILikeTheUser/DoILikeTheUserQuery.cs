using Business.Utilities.Results.Abstract;
using Entities.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Managers.UserLikes.Queries.DoILikeTheUser
{
    public class DoILikeTheUserQuery : IRequest<IResult>
    {
        public int UserId { get; set; }
        public int AcclaimedUserId { get; set; }
    }
}
