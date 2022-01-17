using Business.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class AlbumPhotoDto : IDto
    {
        public int AlbumId { get; set; }
        public int PhotoId { get; set; }
        public bool IsMain { get; set; }
    }
}
