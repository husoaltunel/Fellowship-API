using Core.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Business.Abstract
{
    public interface IConnection
    {
        IDbConnection Connection { get;set;}
    }
}
