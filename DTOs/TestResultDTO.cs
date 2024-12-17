using System.Collections.Generic;
using System;

namespace TestProgrammasy.DTOs
{
    public class TestResultDTO
    {
        public int Id { get; set; }
        public string TestTitle { get; set; }
        public string UserId { get; set; }
        public int Score { get; set; }   
        public string Subject { get; set; }
        public int TotalPoints { get; set; }
        public int EarnedPoints { get; set; }
        public double Percentage { get; set; }
        public string Grade { get; set; }
        public DateTime CompletedDate { get; set; }
    }
}
