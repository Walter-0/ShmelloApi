using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShmelloApi.Models
{
    public class Swimlane
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;
        public int BoardId { get; set; }

        [JsonIgnore]
        public Board Board { get; set; } = null!;
        public ICollection<Card> Cards { get; } = new List<Card>();
    }
}
