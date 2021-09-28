using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Core.DTOs
{
    public class CompanyDTO
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Sector { get; set; }
        public string Vision { get; set; }
        public string Mission { get; set; }
        public string LogoUrl { get; set; }

    }
}
