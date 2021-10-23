using Core.Utilities.Results.Abstract;
using Entities.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Managers.User.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<IDataResult<UserDto>>
    {
        public int Id { get;set;}
    }
}
