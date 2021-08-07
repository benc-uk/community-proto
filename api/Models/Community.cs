using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CommunityApi.Models
{
    public class Community
    {
        // Due to bug in SDK https://github.com/Azure/azure-cosmos-dotnet-v3/issues/2533
        // JsonPropertyName is ignored! So we have to name it "id" for now
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