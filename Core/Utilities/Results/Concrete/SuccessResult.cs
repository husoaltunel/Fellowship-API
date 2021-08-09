using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results.Concrete
{
    public class SuccessResult : Result,IResult
    {     
        public SuccessResult(): base(success:true)
        {

        }
        public SuccessResult(string message):base(success:true,message)
        {

        }      
    }
}
