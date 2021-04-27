using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CallCenter.Agent.Server.Application.Interfaces
{
    public interface ICallCenterDbContext
    {
        DbSet<Shared.Models.Agent> Agents { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
