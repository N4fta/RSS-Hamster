using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class RegisterUser
    {
        [Required(ErrorMessage = "Name is required")]
        [MinLength(5, ErrorMessage = "Name is too short")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(5, ErrorMessage = "Password is too short")]
        public string Password { get; set; }

        // For updating passwords
        [MinLength(5, ErrorMessage = "Password is too short")]
        public string? NewPassword { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}
