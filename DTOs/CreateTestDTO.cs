using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TestProgrammasy.Models;

namespace TestProgrammasy.DTOs
{
    public class CreateTestDTO
    {
        [Required(ErrorMessage = "Test ady hökmany!")]
        public string Name { get; set; }
        public string UserId { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }

        [Required(ErrorMessage = "Test derejesi hökmany!")]
        public string Level { get; set; }

        [Required(ErrorMessage = "Test wagty hökmany!")]
        public int TimeLimit { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public List<CreateQuestionDTO> Questions { get; set; }        
    }
}
