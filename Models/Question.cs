using System;
using System.Collections;
using System.Collections.Generic;

namespace TestProgrammasy.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public int Order { get; set; }
        public DateTime CreatedAt { get; set; }
        public string QuestionType { get; set; }
        public int CorrectAnswerIndex { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public int TestId { get; set; }
        public Test Test { get; set; }
    }
}
