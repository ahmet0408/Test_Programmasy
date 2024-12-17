using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestProgrammasy.DTOs
{
    public class EditQuestionDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Sorag teksti hökmany")]
        public string QuestionText { get; set; }

        //public int Points { get; set; } = 1;

        public List<EditAnswerDTO> Answers { get; set; } = new List<EditAnswerDTO>();
    }
}
