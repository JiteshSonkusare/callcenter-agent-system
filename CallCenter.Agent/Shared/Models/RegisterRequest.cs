using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CallCenter.Agent.Shared.Models
{
    public class RegisterRequest
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z]{2}[0-9]{5}$", ErrorMessage = "User Id is not in correct format! example: <AB12345>")]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match!")]
        public string PasswordConfirm { get; set; }

        public List<UserRole> Roles { get; set; } = null;
    }
}
