using Data.DatabaseConnections;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using WebApp.Models;
using Domain.Services;
using Domain;

namespace WebApp.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public LoginUser LoginUser { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                Domain.User user;
                try
                {
                    user = new UserService().GetUser(LoginUser.Email);
                }
                catch (Exception ex)
                {

                    return Page();
                }

                if (user == null)
                {
                    // Email not found
                    return Page();
                }
                if (!user.VerifyPassword(LoginUser.Password))
                {
                    // Password is wrong
                    return Page();
                }

                // From ppt week 7
                List<Claim> claims = new List<Claim>();
                // The claims with a string as name are easier to find and retreive later
                // The ones that use ClaimTypes are useful for Framework stuff & Authorization
                claims.Add(new Claim("Id", user.Id.ToString()));
                claims.Add(new Claim("Username", user.Username));
                claims.Add(new Claim("HashedPassword", user.HashedPassword));
                claims.Add(new Claim("Name", user.Name));
                claims.Add(new Claim(ClaimTypes.Name, user.Name));
                claims.Add(new Claim("Email", user.Email));
                claims.Add(new Claim(ClaimTypes.Email, user.Email));
                claims.Add(new Claim("Role", user.Role));
                claims.Add(new Claim(ClaimTypes.Role, user.Role));

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));

                return RedirectToPage("/Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
