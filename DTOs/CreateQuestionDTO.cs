using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestProgrammasy.DTOs
{
    public class CreateQuestionDTO
    {
        [Required(ErrorMessage = "Sorag teksti hökmany")]
        public string QuestionText { get; set; }
        public int CorrectAnswerIndex { get; set; }
        public List<CreateAnswerDTO> Answers { get; set; }
    }
}
