using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class LoginUser
    {
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression("^[\\w\\-\\.]+@([\\w-]+\\.)+[\\w-]{2,}$", ErrorMessage = "Email invalid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [Length(7, 100, ErrorMessage = "Password is too short")]
        public string Password { get; set; }
    }
}
