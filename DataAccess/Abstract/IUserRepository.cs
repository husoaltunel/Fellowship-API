using Business.Entities.Concrete;
using Core.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IUserRepository : IRepository<User>
    {
    }
}
