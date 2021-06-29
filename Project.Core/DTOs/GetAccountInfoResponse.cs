using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Core.DTOs
{
    public class GetAccountInfoResponse
    {
        [JsonProperty(PropertyName = "kind")]
        public string Kind { get; set; }

        [JsonProperty(PropertyName = "users")]
        public List<Users> User { get; set; }
    }

    public class Users
    {
        [JsonProperty(PropertyName = "localId")]
        public string LocalId { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "displayName")]
        public string DisplayName { get; set; }

        [JsonProperty(PropertyName = "passwordHash")]
        public string PasswordHash { get; set; }

        [JsonProperty(PropertyName = "emailVerified")]
        public bool EmailVerified { get; set; }

        [JsonProperty(PropertyName = "passwordUpdatedAt")]
        public string PasswordUpdatedAt { get; set; }

        [JsonProperty(PropertyName = "validSince")]
        public string ValidSince { get; set; }

        [JsonProperty(PropertyName = "lastLoginAt")]
        public string LastLoginAt { get; set; }

        [JsonProperty(PropertyName = "createdAt")]
        public string CreatedAt { get; set; }
    }
}
