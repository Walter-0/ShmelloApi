using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShmelloApi.Models
{
    public class Board
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required int UserId { get; set; }
        public required User User { get; set; } = null!;
        public ICollection<Swimlane> Swimlanes { get; } = new List<Swimlane>();
    }
}
