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
        public DateTime StartDate { get; set; } // okuwçynyn teste başlan wagty möhümdir
        public DateTime CompletedAt { get; set; } // testi gutaran wagty hem möhüm
        public int Score {  get; set; } // dogry jogaplaryň sany
        public bool Passed { get; set; } // testi gechdimi?
    }
}
