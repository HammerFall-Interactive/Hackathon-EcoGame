using BCrypt.Net;

using HammerFallInteractive.EcoGame.Server.Models;

using Microsoft.AspNetCore.Mvc;

using System.Security.Cryptography;

namespace HammerFallInteractive.EcoGame.Server.Controllers
{
    [Route("api/v1/users")]
    public class UsersController : Controller
    {
        [HttpPost, Route("signup")]
        public IActionResult CreateUser([FromBody] User user)
        {
            if (string.IsNullOrWhiteSpace(user.Password) || string.IsNullOrWhiteSpace(user.RepeatedPassword) || user.Username != user.RepeatedPassword)
                return BadRequest(new
                {
                    Type = "error",
                    Content = new { Message = "Passwords do not match" }
                });

            if (string.IsNullOrWhiteSpace(user.Email))
                return BadRequest(new
                {
                    Type = "error",
                    Content = new
                    {
                        Message = "Email cannot be empty"
                    }
                });

            user.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(user.Password);
            user.RepeatedPassword = string.Empty;
            user.Id = Guid.NewGuid();

            user.UserPlanet = new Planet();
            user.UserPlanet.Id = Guid.NewGuid();

            using (ApplicationDatabaseContext context = new ApplicationDatabaseContext())
            {
                context.Users.Add(user);
            }
            return View();
        }
    }
}
