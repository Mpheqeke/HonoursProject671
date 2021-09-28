using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Core.DTOs
{
    public class CompanyApplicantsDTO
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Motivation { get; set; }
        public string CVUrl { get; set; }
    }
}
