using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Core.DTOs
{
    public class CompanyRepsDTO
    {
        public int CompanyId { get; set; }
        public int RepId { get; set; }
        public string RepFirstName { get; set; }
        public string RepLastName { get; set; }
        public string RepProfileImageUrl { get; set; }
    }
}
