using CallCenter.Agent.Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace CallCenter.Agent.Shared.Models
{
    public class Agent : AuditableEntity
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]{2}[0-9]{5}$", ErrorMessage = "User Id is not in correct format. example: <AB12345>.")]
        public string UserId { get; set; }
        [Required]
        public string Name { get; set; }
        public int? PhoneNumber { get; set; }
        public string Email { get; set; }
        [Required]
        public int Skill { get; set; }
    }
}
