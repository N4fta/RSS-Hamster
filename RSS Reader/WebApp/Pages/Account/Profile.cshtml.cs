using Data.DatabaseConnections;
using Data;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Domain.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Pages
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        [BindProperty]
        public RegisterUser UpdateUser { get; set; }

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

            if (user != null && user.Email != User.FindFirst("Email").Value) return new JsonResult(false);

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

            if (user != null && user.Username != User.FindFirst("Username").Value) return new JsonResult(false);

            return new JsonResult(true);
        }

        public IActionResult OnPostUpdate()
        {
            if (ModelState.IsValid)
            {
                UserService userService = new UserService();

                User oldUser = userService.GetUser(User.FindFirst("Email").Value);
                User newUser = null;

                if (UpdateUser.NewPassword != null)
                {
                    // Also change password
                    newUser = new User(oldUser.Id, UpdateUser.Name, Username, Email, PasswordHashing.HashPassword(UpdateUser.NewPassword), UpdateUser.Notes, "Reader", oldUser.Categories, oldUser.Reviews);
                }
                else newUser = new User(oldUser.Id, UpdateUser.Name, Username, Email, PasswordHashing.HashPassword(UpdateUser.Password), UpdateUser.Notes, "Reader", oldUser.Categories, oldUser.Reviews);

                if (!oldUser.VerifyPassword(UpdateUser.Password))
                {
                    // Wrong Password
                    return Page();
                }

                if (userService.Update(newUser).Success)
                {
                    HttpContext.SignOutAsync();

                    // Change Claims
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim("Id", newUser.Id.ToString()));
                    claims.Add(new Claim("Username", newUser.Username));
                    claims.Add(new Claim("HashedPassword", newUser.HashedPassword));
                    claims.Add(new Claim("Name", newUser.Name));
                    claims.Add(new Claim(ClaimTypes.Name, newUser.Name));
                    claims.Add(new Claim("Email", newUser.Email));
                    claims.Add(new Claim(ClaimTypes.Email, newUser.Email));
                    claims.Add(new Claim("Role", newUser.Role));
                    claims.Add(new Claim(ClaimTypes.Role, newUser.Role));

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));
                    return Redirect("/Account/Profile");
                }
            }
            // Model not valid
            return Page();
        }

        public IActionResult OnPostDelete()
        {
            if (ModelState.IsValid)
            {
                UserService userService = new UserService();

                User user = userService.GetUser(User.FindFirst("Email").Value);

                if (!user.VerifyPassword(UpdateUser.Password))
                {
                    // Wrong Password
                    return Page();
                }

                if (userService.Delete(user).Success)
                {
                    return Redirect("/Account/Logout");
                }
                else
                {
                    // smt went wrong
                }
            }
            return Page();
        }
    }
}
