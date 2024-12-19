using System;
using System.Collections.Generic;
using TestProgrammasy.Models;

namespace TestProgrammasy.DTOs
{
    public class CreateTestDTO
    {
        public string Name { get; set; }
        public string UserId { get; set; }
        public string Description { get; set; }
        public string Level { get; set; }
        public int TimeLimit { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public List<CreateQuestionDTO> Questions { get; set; }        
    }
}
