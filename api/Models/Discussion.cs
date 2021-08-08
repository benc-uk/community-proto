using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

// Due to bug in SDK https://github.com/Azure/azure-cosmos-dotnet-v3/issues/2533
// JsonPropertyName is ignored! So we have to use lowercase property names

namespace CommunityApi.Models
{
    public class Discussion
    {
        [JsonPropertyName("id")]
        public string id { get; set; }

        [Required]
        [JsonPropertyName("title")]
        public string title { get; set; }

        [Required]
        [JsonPropertyName("icon")]
        public string icon { get; set; }

        [Required]
        [JsonPropertyName("body")]
        public string body { get; set; }

        [JsonPropertyName("created")]
        public string created { get; set; }

        [Required]
        [JsonPropertyName("community")]
        public string community { get; set; }
    }
}