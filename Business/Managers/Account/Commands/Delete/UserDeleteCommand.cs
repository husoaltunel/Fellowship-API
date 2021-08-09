using Core.Utilities.Results.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Managers.Account.Commands.Delete
{
    public class UserDeleteCommand : IRequest<IDataResult<int>>
    {
        public int Id { get;set;}
    }
}
