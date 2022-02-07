using Business.Utilities.Results.Abstract;
using Entities.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Managers.UserLikes.Queries.GetUserLikesByUsername
{
    public class GetUserLikesByUsernameQuery : IRequest<IDataResult<UserLikeDto>>
    {
        public int UserId { get; set; }
    }
}
