using System.Collections.Generic;

namespace TestProgrammasy.DTOs
{
    public class QuestionPreviewDTO
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public int Points { get; set; }
        public List<string> Answers { get; set; }

    }
}
