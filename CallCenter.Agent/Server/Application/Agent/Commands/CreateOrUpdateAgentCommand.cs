using CallCenter.Agent.Server.Application.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CallCenter.Agent.Server.Application.Agent.Commands
{
    public class CreateOrUpdateAgentCommand : IRequest<int>
    {
        public int? Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public int? PhoneNumber { get; set; }
        public string Email { get; set; }
        public int Skill { get; set; }
        public string CreatedBy { get; set; }

        public class Handler : IRequestHandler<CreateOrUpdateAgentCommand, int>
        {
            private readonly ICallCenterDbContext _context;

            public Handler(ICallCenterDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateOrUpdateAgentCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var entity = new Shared.Models.Agent();

                    if (request.Id != 0)
                        entity = await _context.Agents.FindAsync(request.Id);
                    else
                        entity = new Shared.Models.Agent();

                    entity.UserId = request.UserId;
                    entity.Name = request.Name;
                    entity.PhoneNumber = request.PhoneNumber;
                    entity.Email = request.Email;
                    entity.Skill = request.Skill;
                    entity.CreatedBy = request.CreatedBy ?? "Swagger";
                    entity.Created = DateTime.Now;

                    _context.Agents.Add(entity);
                    await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                    return entity.Id;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
