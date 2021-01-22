using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PhotoGallery.Shared.ApiModels.Api
{
    public class ErrorValidationResponse
    {
        [JsonPropertyName("errors")]
        public Dictionary<string, string[]> ModelStateErrors { get; set; }
    }
}