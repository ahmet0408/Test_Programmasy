using System;
using TestProgrammasy.Models;

namespace TestProgrammasy.DTOs
{
    public class CreateTestDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public TestLevel Level { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
