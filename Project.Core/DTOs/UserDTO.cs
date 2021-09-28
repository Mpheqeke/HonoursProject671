using System.Collections.Generic;

namespace Project.Core.DTOs
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Gender { get; set; }
        public string ImageUrl { get; set; }
        public string Email { get; set; }
    }
}
