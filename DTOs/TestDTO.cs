using System.Collections.Generic;

namespace TestProgrammasy.DTOs
{
    public class TestDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TotalQuestions { get; set; }
        public int TotalPoints { get; set; }
        public int TimeLimit { get; set; }
        public List<QuestionDTO> Questions { get; set; }

    }
}
