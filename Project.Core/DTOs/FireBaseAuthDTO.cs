using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Core.DTOs
{
    public class FireBaseAuthDTO
    {
        public string password { get; set; }
        public string email { get; set; }
        public bool returnSecureToken { get; set; }
    }
}
