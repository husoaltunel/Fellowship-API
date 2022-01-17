using Business.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class PhotoDto : BaseDto, IDto
    {
        public FileContentResult PhotoFile { get;set;}
    }
}
