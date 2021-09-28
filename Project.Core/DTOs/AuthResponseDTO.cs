using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Core.DTOs
{
    public class AuthResponseDTO
    {
        public string idToken { get; set; }
        public string email { get; set; }
        public string refreshToken { get; set; }
        public string expiresIn { get; set; }
        public string LocalId { get; set; }
        public bool registered { get; set; }
    }
}
