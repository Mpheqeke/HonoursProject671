using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Core.DTOs
{
    public class UserAuthenticationResponse
    {
        [JsonProperty(PropertyName = "kind")]
        public string Kind { get; set; }

        [JsonProperty(PropertyName = "idToken")]
        public string IdToken { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "refreshToken")]
        public string RefreshToken { get; set; }

        [JsonProperty(PropertyName = "expiresIn")]
        public string ExpiresIn { get; set; }

        [JsonProperty(PropertyName = "localId")]
        public string LocalId { get; set; }
    }
}
