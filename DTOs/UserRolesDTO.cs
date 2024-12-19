using System.Collections.Generic;

namespace TestProgrammasy.DTOs
{
    public class UserRolesDTO
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string UserType { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
