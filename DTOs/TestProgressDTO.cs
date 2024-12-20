using System;

namespace TestProgrammasy.DTOs
{
    public class TestProgressDTO
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public string UserId { get; set; }
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public DateTime StartDate { get; set; }
    }
}
