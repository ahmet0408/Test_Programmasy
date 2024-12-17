using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestProgrammasy.DTOs
{
    public class QuestionDTO
    {
        public int Id { get; set; }

        public string QuestionText { get; set; }
        public int Points { get; set; }

        public List<AnswerDTO> Answers { get; set; }
    }
}
