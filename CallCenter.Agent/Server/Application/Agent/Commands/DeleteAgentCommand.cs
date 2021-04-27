using CallCenter.Agent.Server.Application.Interfaces;
using CallCenter.Agent.Server.Common.Exceptions;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CallCenter.Agent.Server.Application.Agent.Commands
{
    public class DeleteAgentCommand : IRequest
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<DeleteAgentCommand>
        {
            private readonly ICallCenterDbContext _context;

            public Handler(ICallCenterDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteAgentCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var entity = await _context.Agents.FindAsync(request.Id);

                    if (entity == null)
                    {
                        throw new NotFoundException(nameof(Shared.Models.Agent), request.Id);
                    }

                    _context.Agents.Remove(entity);
                    await _context.SaveChangesAsync(cancellationToken);

                    return Unit.Value;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
