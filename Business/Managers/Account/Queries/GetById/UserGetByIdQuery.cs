using Core.Utilities.Results.Abstract;
using Entities.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Managers.Account.Queries.GetById
{
    public class UserGetByIdQuery : IRequest<IDataResult<UserViewDto>>
    {
        public int Id { get;set;}
    }
}
