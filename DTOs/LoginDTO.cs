using System.ComponentModel.DataAnnotations;

namespace TestProgrammasy.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Ulanyjy adyňyzy girizmegiňiz zerur")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Paroly girizmegiňiz zerur")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Meni ýatda sakla")]
        public bool RememberMe { get; set; }
    }
}
