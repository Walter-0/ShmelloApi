using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShmelloApi.Models
{
    public class Board
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;
        public int UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }
        public ICollection<Swimlane> Swimlanes { get; } = new List<Swimlane>();
    }
}
