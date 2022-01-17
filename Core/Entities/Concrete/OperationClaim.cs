using Business.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Entities.Concrete
{
    public class OperationClaim : BaseEntity,IEntity
    {
        public string Name { get;set;}
    }
}
