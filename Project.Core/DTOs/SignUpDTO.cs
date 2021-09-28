using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Core.DTOs
{
    public class SignUpDTO
    {
        public string UserFirebaseId { get; set; }
        public string JwtToken { get; set; }
    }
}
