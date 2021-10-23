using Core.Entities.Abstract;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class AlbumPhoto : BaseEntity,IEntity
    {
        public int AlbumId { get;set;}
        public int PhotoId { get;set;}
        public bool IsMain { get;set;}
    }
}
