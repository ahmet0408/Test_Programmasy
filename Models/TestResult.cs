using System;
using System.Globalization;

namespace TestProgrammasy.Models
{
    public class TestResult
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ApplicationUser User { get; set; }
        public int TestId { get; set; }
        public Test Test { get; set; }
        public DateTime StartDate { get; set; } // okuwçynyn teste başlan wagty möhümdir
        public DateTime CompletedAt { get; set; } // testi gutaran wagty hem möhüm
        public int Score {  get; set; } // dogry jogaplaryň sany
        public int TotalPoints { get; set; } 
        public double Percentage { get; set; }
        public string Grade { get; set; }
    }
}
