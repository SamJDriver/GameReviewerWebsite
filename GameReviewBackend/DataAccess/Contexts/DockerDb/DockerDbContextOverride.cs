using DataAccess.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Contexts.DockerDb
{
    public partial class DockerDbContext : DbContext
    {

        private static string _userId { get; set; }

        public static void SetCreatedByUserId(string userId)
        {
            _userId = userId;
        }
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
                    track.CreatedBy = _userId;
                    track.CreatedDate = DateTime.UtcNow;
                }
            }
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {

            await Task.Run(() =>
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
                        track.CreatedBy = _userId;
                        track.CreatedDate = DateTime.UtcNow;
                    }
                }
            });

            return await base.SaveChangesAsync();

        }
    }
}