using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShmelloApi.Models
{
    public class Card
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Body { get; set; } = string.Empty;
        public int SwimlaneId { get; set; }

        [JsonIgnore]
        public Swimlane Swimlane { get; set; } = null!;
        public int UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; } = null!;
    }
}
