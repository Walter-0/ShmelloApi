using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShmelloApi.Models;

namespace ShmelloApi.DTOs
{
    public record struct BoardCreateDto(string Title, int UserId);
}
