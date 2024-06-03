using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Contexts.DockerDb
{
    public partial class DockerDbContext : DbContext
    {
        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            var added = this.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added)
                .Select(e => e.Entity)
                .ToArray();

            foreach (var entity in added)
            {
                if (entity is ITrackable)
                {
                    var track = entity as ITrackable;
                    track.CreatedBy = "System";
                    track.CreatedDate = DateTime.Now;
                }
            }
            return base.SaveChanges();
        }
    }
}