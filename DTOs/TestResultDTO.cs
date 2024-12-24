using System.Collections.Generic;
using System;

namespace TestProgrammasy.DTOs
{
    public class TestResultDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int StudentClass { get; set; }
        public string UserId { get; set; }
        public string StudentName { get; set; }
        public int TestId { get; set; }
        public int Score { get; set; }   
        public string Description { get; set; }
        public int TotalPoints { get; set; }
        public double Percentage { get; set; }
        public string Grade { get; set; }
        public DateTime CompletedAt { get; set; }
    }
}
