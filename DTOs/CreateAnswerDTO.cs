using System.ComponentModel.DataAnnotations;

namespace TestProgrammasy.DTOs
{
    public class CreateAnswerDTO
    {
        [Required(ErrorMessage = "Jogap teksti hökmany")]
        public string AnswerText { get; set; }
        public int Order { get; set; }
        public bool IsCorrect { get; set; }
    }
}
