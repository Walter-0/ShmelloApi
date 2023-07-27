using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShmelloApi.Models
{
    public class Card
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = "";

        [Required]
        public string Body { get; set; } = "";
        public int SwimlaneId { get; set; }
        public int UserId { get; set; }
    }
}
