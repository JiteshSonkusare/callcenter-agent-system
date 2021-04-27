using System;

namespace CallCenter.Agent.Server.Application.Agent.Queries
{
    public class AgentDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public int? PhoneNumber { get; set; }
        public string Email { get; set; }
        public int Skill { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
    }
}
