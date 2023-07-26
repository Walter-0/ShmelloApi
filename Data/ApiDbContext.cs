using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShmelloApi.Models;

namespace ShmelloApi.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Board> Boards { get; set; }

        // public DbSet<UserBoard> UserBoards { get; set; }
        public DbSet<Swimlane> Swimlanes { get; set; }
        public DbSet<Card> Cards { get; set; }
    }
}
