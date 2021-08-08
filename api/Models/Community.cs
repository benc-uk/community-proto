using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

// Due to bug in SDK https://github.com/Azure/azure-cosmos-dotnet-v3/issues/2533
// JsonPropertyName is ignored! So we have to use lowercase property names

namespace CommunityApi.Models
{
    public class Community
    {
        [JsonPropertyName("id")]
        public string id { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string description { get; set; }

        [Required]
        public string image { get; set; }

        public string[] members { get; set; }
    }
}