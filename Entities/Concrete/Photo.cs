using Business.Entities.Abstract;
using Business.Entities.Concrete;
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
