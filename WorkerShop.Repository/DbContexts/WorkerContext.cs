using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerShop.Repository.Entities;

namespace WorkerShop.Repository.DbContexts
{
    public class WorkerContext : DbContext
    {
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Client>  Clients { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<WorkOrder> WorkOrders { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }   
        public WorkerContext(DbContextOptions options)
            :base(options)
        {

        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var modifiedEntities = ChangeTracker.Entries().
                Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted).ToList();

            foreach(var entity in modifiedEntities) 
            {
                var log = new AuditLog
                {
                    EntityName = entity.Entity.GetType().Name,
                    Action = entity.State.ToString(),
                    TimeStamp = DateTime.UtcNow,
                    Changes = GetChanges(entity)
                };
                AuditLogs.Add(log);
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        private string GetChanges(EntityEntry modifiedEntity)
        {
            var changes = new StringBuilder();
            foreach (var property in modifiedEntity.OriginalValues.Properties)
            {
                var originalValue = modifiedEntity.OriginalValues[property];
                var currentValue = modifiedEntity.CurrentValues[property];

                if (!Equals(originalValue, currentValue))
                {
                    changes.Append($"{property.Name}: From '{originalValue}' to '{currentValue}'.");
                }
            }
            return changes.ToString();
        }

    }
}
