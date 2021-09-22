using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results.Concrete
{
    public class SuccessDataResult<TEntity> : DataResult<TEntity>,IDataResult<TEntity>
    {
       
        public SuccessDataResult():base(success:true)
        {

        }
        public SuccessDataResult(string message):base(success:true,message)
        {

        }
        public SuccessDataResult(TEntity data):base(data,success:true)
        {

        }
        public SuccessDataResult(TEntity data,string message):base(data,success:true,message)
        {

        }
    }
}
