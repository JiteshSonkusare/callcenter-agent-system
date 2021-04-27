using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using CallCenter.Agent.Server.Application.Interfaces;
using CallCenter.Agent.Server.Common.Interfaces;

namespace CallCenter.Agent.Server.Application.Agent.Queries
{
    public class GetAgentsListQuery : IRequest<IEnumerable<AgentDto>>
    {
        public class GetAgentsListQueryHandler : IRequestHandler<GetAgentsListQuery, IEnumerable<AgentDto>>
        {
            private readonly ICallCenterDbContext _context;
            private readonly IMapper<Shared.Models.Agent, AgentDto> _mapper;

            public GetAgentsListQueryHandler(ICallCenterDbContext context, IMapper<Shared.Models.Agent, AgentDto> mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IEnumerable<AgentDto>> Handle(GetAgentsListQuery request, CancellationToken cancellationToken)
            {
                var entities = await _context.Agents.ToListAsync();

                if (entities == null) return null;

                var agents = _mapper.Map(entities);
                return agents;
            }
        }
    }
}
