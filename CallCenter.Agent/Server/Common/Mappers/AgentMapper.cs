using CallCenter.Agent.Server.Application.Agent.Queries;
using CallCenter.Agent.Server.Common.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CallCenter.Agent.Server.Common.Mappers
{
    public class AgentMapper : IMapper<Shared.Models.Agent, AgentDto>
    {
        public AgentDto Map(Shared.Models.Agent entity)
        {
            return new AgentDto
            {
                Id          = entity.Id,
                UserId      = entity.UserId,
                Name        = entity.Name,
                PhoneNumber = entity.PhoneNumber,
                Email       = entity.Email,
                Skill       = entity.Skill,
                CreatedBy   = entity.CreatedBy,
                Created     = entity.Created
            };
        }

        public IEnumerable<AgentDto> Map(IEnumerable<Shared.Models.Agent> entities)
        {
            return (from agent in entities
                    select Map(agent)).ToList();
        }
    }
}
