using CallCenter.Agent.Server.Application.Interfaces;
using CallCenter.Agent.Server.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CallCenter.Agent.Server.Application.Agent.Queries
{
    public class GetAgentDetailQuery : IRequest<AgentDto>
    {
        public int Id { get; set; }

        public class GetAgentDetailQueryHandler : IRequestHandler<GetAgentDetailQuery, AgentDto>
        {
            private readonly ICallCenterDbContext _context;
            private readonly IMapper<Shared.Models.Agent, AgentDto> _mapper;

            public GetAgentDetailQueryHandler(ICallCenterDbContext context, IMapper<Shared.Models.Agent, AgentDto> mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<AgentDto> Handle(GetAgentDetailQuery request, CancellationToken cancellationToken)
            {
                var entity = await _context.Agents
                    .Where(a => a.Id.Equals(request.Id))
                    .FirstOrDefaultAsync(cancellationToken);

                if (entity == null) return null;

                var agent = _mapper.Map(entity);
                return agent;
            }
        }
    }
}
