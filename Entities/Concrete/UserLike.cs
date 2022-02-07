using Business.Entities.Abstract;
using Business.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class UserLike : BaseEntity,IEntity
    {
        public int AcclaimedUserId { get;set;}
        public int UserLikedId { get;set;}
    }
}
