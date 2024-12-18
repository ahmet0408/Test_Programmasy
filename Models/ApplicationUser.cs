using Microsoft.AspNetCore.Identity;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
using System;

namespace TestProgrammasy.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserType { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string AvatarUrl { get; set; }
        public ICollection<Test> Tests { get; set; }
        public ICollection<TestResult> TestResults { get; set; }

    }
}
