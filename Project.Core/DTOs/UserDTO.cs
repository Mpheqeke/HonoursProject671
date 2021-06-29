using System.Collections.Generic;

namespace Project.Core.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public bool IsActive { get; set; }
        public string LocalId { get; set; }
        public string ThumbnailUrl { get; set; }
        public string ImageUrl { get; set; }
        public string Gender { get; set; }
        public string displayName => $"{UserFirstName}{UserLastName}";
    }
}
