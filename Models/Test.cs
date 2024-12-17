using System;
using System.Collections;
using System.Collections.Generic;

namespace TestProgrammasy.Models
{
    public class Test
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TestLevel Level { get; set; } // test derejesi
        public DateTime CreatedAt { get; set; } //test goshulan wagty
        public DateTime StartDate { get; set; } //test bashlayan wagty
        public DateTime EndDate { get; set; } //test gutaryan wagty
        public string UserId { get; set; } //kim tarapyndan goshuldy
        public ApplicationUser User { get; set; }
        public ICollection<Question> Questions { get; set; } // teste degishli soraglar
        public ICollection<TestResult> TestResults { get; set; }

    }
}
