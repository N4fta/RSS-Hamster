using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using Domain;
using Domain.Services;
using Data;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Pages
{
    public class FormModel : PageModel
    {

        [BindProperty]
        public RegisterUser RegisterUser { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Username is required")]
        [MinLength(5, ErrorMessage = "Username is too short")]
        [PageRemote(
        ErrorMessage = "Username already exists",
        AdditionalFields = "__RequestVerificationToken",
        HttpMethod = "post",
        PageHandler = "CheckUsername")]
        public string Username { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression("^[\\w\\-\\.]+@([\\w-]+\\.)+[\\w-]{2,}$", ErrorMessage = "Email invalid")]
        [PageRemote(
        ErrorMessage = "Email address already exists",
        AdditionalFields = "__RequestVerificationToken",
        HttpMethod = "post",
        PageHandler = "CheckEmail")]
        public string Email { get; set; }

        public void OnGet()
        {

        }


        // Email & Username duplicate validation without full posting (partial unobstructive validation)
        public JsonResult OnPostCheckEmail()
        {
            Domain.User user = null;
            try
            {
                user = new UserService().GetUser(Email);
            }
            catch { return new JsonResult("Connection error"); }

            if (user != null) return new JsonResult(false);

            return new JsonResult(true);
        }
        public JsonResult OnPostCheckUsername()
        {
            Domain.User user = null;
            try
            {
                user = null;
                //user = new UserService().GetUser(Email);
            }
            catch { return new JsonResult("Connection error"); }

            if (user != null) return new JsonResult(false);

            return new JsonResult(true);
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                UserService userService = new();

                User newUser = new User(RegisterUser.Name, Username, Email, PasswordHashing.HashPassword(RegisterUser.Password), RegisterUser.Notes, "Reader");

                DBResult dBResult = userService.Add(newUser);

                if (dBResult.Success)
                {
                    // All good
                    return RedirectToPage("/Account/Login");
                }
                // Inform user
            }
            return Page();
        }
    }
}
