using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results.Concrete
{
    public class DataResult<TEntity> : Result, IDataResult<TEntity>
    {
        public TEntity Data { get; set; }

        public DataResult(bool success) : base(success)
        {

        }
        public DataResult(bool success, string message) : base(success, message)
        {

        }
        public DataResult(TEntity data, bool success) : this(success)
        {
            Data = data;
        }
        public DataResult(TEntity data, bool success, string message) : this(success, message)
        {
            Data = data;
        }


    }
}
