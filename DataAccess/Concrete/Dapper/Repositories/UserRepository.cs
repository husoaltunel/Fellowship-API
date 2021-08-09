using Core.DataAccess.Concrete;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataAccess.Concrete.Dapper.Repositories
{
    public class UserRepository : DpBaseRepository<User>,IUserRepository
    {
        public UserRepository(IDbConnection dbConnection,IDbTransaction dbTransaction): base(dbConnection,dbTransaction)
        {

        }
    }
}
