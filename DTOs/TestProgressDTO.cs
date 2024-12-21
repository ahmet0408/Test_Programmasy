using System;
using System.Collections.Generic;

namespace TestProgrammasy.DTOs
{
    public class TestProgressDTO
    {
        public int TestId { get; set; }
        public string UserId { get; set; }
        public DateTime StartDate { get; set; }
        public Dictionary<int, int> Answers { get; set; } = new Dictionary<int, int>(); // QuestionId, AnswerId
    }
}
