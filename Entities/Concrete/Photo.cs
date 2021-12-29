using Core.Entities.Abstract;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Photo : BaseEntity, IEntity
    {
        public string Url { get;set;}
    }
}
