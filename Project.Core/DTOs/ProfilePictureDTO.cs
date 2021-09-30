using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Core.DTOs
{
    public class ProfilePictureDTO
    {
        public IFormFile ImageUrl { get; set; }
    }
}
