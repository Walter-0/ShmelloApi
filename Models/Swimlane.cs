using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShmelloApi.Models
{
    public class Swimlane
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = "";
        public int BoardId { get; set; }
        public ICollection<Card> Cards { get; } = new List<Card>();
    }
}
