using System;
using System.Collections.Generic;

namespace TestProgrammasy.DTOs
{
    public class TestDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Level { get; set; }
        public string UserId { get; set; }
        public QuestionDTO CurrentQuestion { get; set; }
        public int TimeLimit { get; set; }
        public List<QuestionDTO> Questions { get; set; }

    }
}
