using System;
using System.Collections.Generic;
using TestProgrammasy.Models;

namespace TestProgrammasy.DTOs
{
    public class EditTestDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TestLevel Level { get; set; }

        //public int Duration { get; set; } // Minutda
        //public DateTime CreatedDate { get; set; }
        public List<EditQuestionDTO> Questions { get; set; }
    }
}
