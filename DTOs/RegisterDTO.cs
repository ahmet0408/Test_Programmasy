using System.ComponentModel.DataAnnotations;

namespace TestProgrammasy.DTOs
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Adyňyzy girizmegiňiz zerur")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Familiýaňyzy girizmegiňiz zerur")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Ulanyjy adyňyzy girizmegiňiz zerur")]
        public string UserType { get; set; }
        [Required(ErrorMessage = "Paroly girizmegiňiz zerur")]
        public string Password { get; set; }
    }
}
