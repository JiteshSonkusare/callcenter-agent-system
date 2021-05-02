using System;
using System.Threading;
using System.Threading.Tasks;
using CallCenter.Agent.Server.Application.Interfaces;
using CallCenter.Agent.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace CallCenter.Agent.Server.Persistence
{
    public class CallCenterDbContext : DbContext, ICallCenterDbContext
    {
        public CallCenterDbContext(DbContextOptions<CallCenterDbContext> options)
            : base(options)
        {
        }

        public DbSet<Shared.Models.Agent> Agents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CallCenterDbContext).Assembly);
        }
    }
}
