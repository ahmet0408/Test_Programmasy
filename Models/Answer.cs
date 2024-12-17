namespace TestProgrammasy.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string AnswerText { get; set; }
        public bool IsCorrect { get; set; }
        public int Order { get; set; }
        public Question Question { get; set; }
        public int QuestionId { get; set; }
    }
}
