using System;
using System.Collections;
using System.Collections.Generic;

namespace TestProgrammasy.Models
{
    public class Test
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Level { get; set; } // test derejesi
        public DateTime CreatedAt { get; set; } //test goshulan wagty
        public DateTime UpdatetAt { get; set; } //test uytgedile wagty
        public int TimeLimit { get; set; }
        public string UserId { get; set; } //kim tarapyndan goshuldy
        public ApplicationUser User { get; set; }
        public ICollection<Question> Questions { get; set; } // teste degishli soraglar
        public ICollection<TestResult> TestResults { get; set; }

    }
}
