using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShmelloApi.Models
{
    public class Card
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Body { get; set; }
        public required int SwimlaneId { get; set; }
        public required Swimlane Swimlane { get; set; } = null!;
        public required int UserId { get; set; }
        public required User User { get; set; } = null!;
    }
}
