﻿using CallCenter.Agent.Shared.Models;

namespace CallCenter.Agent.Shared.Models
{
    public class Agent : AuditableEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public int? PhoneNumber { get; set; }
        public string Email { get; set; }
        public int Skill { get; set; }
    }
}
