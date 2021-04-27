using System;
using System.Threading;
using System.Threading.Tasks;
using CallCenter.Agent.Server.Application.Interfaces;
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

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToen)
        {
            foreach (var entry in ChangeTracker.Entries<Shared.Models.AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = "Test";
                        entry.Entity.Created = DateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToen);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CallCenterDbContext).Assembly);
        }
    }
}
