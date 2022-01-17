using Business.Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Entities.Dtos
{
    public class InsertUserPhotosDto : IDto
    {
        public string UserId { get; set; }
        public IFormCollection PhotoFiles { get; set; }
    }
}
