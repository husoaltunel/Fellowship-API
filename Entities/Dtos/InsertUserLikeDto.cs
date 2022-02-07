using Business.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class InsertUserLikeDto : IDto
    {
        public int AcclaimedUserId { get; set; }
        public int UserLikedId { get; set; }
    }
}
