using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CallCenter.Agent.Shared.Models
{
    public class LoginRequest
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z]{2}[0-9]{5}$", ErrorMessage = "Not a valid user id!")]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
