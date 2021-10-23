using Core.Utilities.Results.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Managers.User.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<IDataResult<int>>
    {
        public int Id { get;set;}
    }
}
