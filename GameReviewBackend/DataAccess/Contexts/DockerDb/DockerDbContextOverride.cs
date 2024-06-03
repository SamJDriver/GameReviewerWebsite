using DataAccess.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Contexts.DockerDb
{
    public partial class DockerDbContext : DbContext
    {

        private static string _username { get; set; } = "System";

        public static void SetUsername(string username)
        {
            _username = username;
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
                    track.CreatedBy = _username;
                    track.CreatedDate = DateTime.Now;
                }
            }
            return base.SaveChanges();
        }
    }
}