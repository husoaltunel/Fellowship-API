using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Core.DataAccess.Abstract
{
    public interface IUnitOfWork : IConnection
    {
        public void OpenConnection();
        public void BeginTransaction();
        public void Commit();
        public void RollBack();
    }
}
