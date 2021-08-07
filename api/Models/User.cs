using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CommunityApi.Models
{
    public class User
    {
        // Due to bug in SDK https://github.com/Azure/azure-cosmos-dotnet-v3/issues/2533
        // JsonPropertyName is ignored! So we have to use lowercase property names
        [JsonPropertyName("id")]
        public string id { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string uid { get; set; }

        public string about { get; set; }

        public string avatar { get; set; }

        public string[] communities { get; set; }
    }
}