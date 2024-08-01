using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DataAccess.Contexts.DockerDb;

public class TrackableEntityInterceptor : SaveChangesInterceptor
{
    private readonly string _userId;

    public TrackableEntityInterceptor(string userId) => this._userId = userId; 

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        DbContext? dbContext = eventData.Context;

        if (dbContext is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        IEnumerable<EntityEntry<ITrackable>> entries = dbContext.ChangeTracker.Entries<ITrackable>();
        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(e => e.CreatedBy).CurrentValue = _userId;
                entry.Property(e => e.CreatedDate).CurrentValue = DateTime.Now;
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

}
