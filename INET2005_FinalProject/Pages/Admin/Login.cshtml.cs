using Azure.Core;
using Azure.Identity;
using INET2005_FinalProject.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Text;
using System.Security.Claims;

namespace INET2005_FinalProject.Pages.Admin
{
    public class LoginModel : PageModel
    {
        private IConfiguration _configuration;

        string? username;
        string? password;

        public LoginModel(IConfiguration iconfig)
        {
            _configuration = iconfig;
        }

        public IActionResult OnPost()
        {
            // Get the username and password values from the webpage
            username = Request.Form["username"];
            password = Request.Form["password"];

            // check config file to see if username+password doesn't match
            if (username != _configuration.GetValue<string>("LoginInfo:Username") || password != _configuration.GetValue<string>("LoginInfo:Password")) 
            { 
                return Page();
            }

            // Setup session
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "admin"),
                new Claim(ClaimTypes.Role, "administrator")
            };

            var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties());

            // Redirect to start page
            return RedirectToPage("/Index");
        }
    }
}
