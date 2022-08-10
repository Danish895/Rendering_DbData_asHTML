using System;
using System.Collections.Generic;

namespace StudentAPI.Models
{
    public partial class StudentDetail
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public string? IsCreatedAt { get; set; }
    }
}
