using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Core.DTOs
{
    public class UserAppliDetailsDTO
    {
        public int CompId { get; set; }
        public int AppliId { get; set; }
        public string CompanyName { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string Location { get; set; }
        public string Responsibilities { get; set; }
        public DateTime StartDate { get; set; }
        public string ApplicationStatus { get; set; }

    }
}
