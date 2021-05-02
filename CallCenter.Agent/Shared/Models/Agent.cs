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
        [RegularExpression(@"\d{10}", ErrorMessage = "Phone number should be 10 digit.")]
        public int? PhoneNumber { get; set; }
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Email address is invalid.")]
        public string Email { get; set; }
        [Required]
        [Range(1, 4, ErrorMessage = "Please select a skill.")]
        public int Skill { get; set; } = 0;
    }
}
