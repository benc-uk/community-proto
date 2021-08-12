using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CommunityApi.Models
{
    public class Community
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Image { get; set; }

        [NotMapped]
        public int DiscussionCount { get; set; }
        [NotMapped]
        public int MemberCount { get; set; }

        [JsonIgnore]
        public List<Discussion> Discussions { get; set; }
        [JsonIgnore]
        public List<User> Members { get; set; }
    }
}