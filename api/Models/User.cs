using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CommunityApi.Models
{
    public class User
    {
        [Required]
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string About { get; set; }

        public string Avatar { get; set; }

        [JsonIgnore]
        public List<Community> Communities { get; set; }
    }
}