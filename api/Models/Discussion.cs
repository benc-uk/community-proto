using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CommunityApi.Models
{
    public class Discussion
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Icon { get; set; }

        [Required]
        public string Body { get; set; }

        public string Created { get; set; }

        public Community Community { get; set; }
    }
}