using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Core.DTOs
{
    public class GoogleErrorResponse
    {
        [JsonProperty(PropertyName = "error")]
        public ErrorMessage Error { get; set; }
    }

    public class ErrorMessage
    {
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }
}
