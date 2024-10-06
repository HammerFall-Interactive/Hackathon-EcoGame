using System.ComponentModel.DataAnnotations;

namespace HammerFallInteractive.EcoGame.Server.Models
{
    public class Planet
    {
        [Key]
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public User Owner { get; set; }
        public string Name { get; set; }
        public DateTime CreationTime { get; set; }
        
        // TODO: add planet properties
    }
}
