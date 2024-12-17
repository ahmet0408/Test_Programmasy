using System.ComponentModel.DataAnnotations;

namespace TestProgrammasy.DTOs
{
    public class AnswerDTO
    {
        public int Id { get; set; }

        public string AnswerText { get; set; }

        public bool IsCorrect { get; set; }
    }
}
