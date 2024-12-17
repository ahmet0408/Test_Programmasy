using System;

namespace TestProgrammasy.Models
{
    public class TestResult
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int TestId { get; set; }
        public Test Test { get; set; }
        public DateTime CompletedAt { get; set; } // testi gutaran wagty
        public int Score {  get; set; } // testin netijesi
        public bool Passed { get; set; } // testi gechdimi?
    }
}
