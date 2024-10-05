using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HammerFallInteractive.EcoGame.Server.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        [NotMapped]
        public string RepeatedPassword { get; set; }

        public int UserPlanetId { get; set; }
        public Planet UserPlanet { get; set; }
    }
}
