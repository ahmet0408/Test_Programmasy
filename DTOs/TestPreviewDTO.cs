using System.Collections.Generic;

namespace TestProgrammasy.DTOs
{
    public class TestPreviewDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TimeLimit { get; set; }
        public string Level { get; set; }
        public int TotalQuestions { get; set; }
        public int TotalPoints { get; set; }
        public List<QuestionPreviewDTO> Questions { get; set; }
    }
}
