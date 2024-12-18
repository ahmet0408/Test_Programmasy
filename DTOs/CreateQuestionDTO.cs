using System.Collections.Generic;

namespace TestProgrammasy.DTOs
{
    public class CreateQuestionDTO
    {
        public string QuestionText { get; set; }
        public int CorrectAnswerIndex { get; set; }
        public List<CreateAnswerDTO> Answers { get; set; }
    }
}
