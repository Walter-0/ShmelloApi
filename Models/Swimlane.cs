using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShmelloApi.Models
{
    public class Swimlane
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required int BoardId { get; set; }
        public required Board Board { get; set; } = null!;
        public ICollection<Card> Cards { get; } = new List<Card>();
    }
}
