using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShmelloApi.Models
{
    public class User
    {
        public int Id { get; set; }

        public required string Email { get; set; }
        public required string Name { get; set; }
        public required string Password { get; set; }
        public ICollection<Board> Boards { get; } = new List<Board>();
        public ICollection<Card> Cards { get; } = new List<Card>();
    }
}
