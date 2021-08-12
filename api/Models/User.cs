using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        public List<Community> Communities { get; set; }
    }
}