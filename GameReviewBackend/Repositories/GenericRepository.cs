using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repositories
{
    public class GenericRepository<TContext> : IGenericRepository<TContext> where TContext : DbContext
    {
        protected readonly TContext _dbContext;

        public GenericRepository(TContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        #region Reads
        public TEntityType GetById<TEntityType>(int id) where TEntityType : class
        {
            DbSet<TEntityType> dbSet = _dbContext.Set<TEntityType>();
            TEntityType entityObject = dbSet.Find(id);
            return entityObject;
        }

        public IQueryable<TEntityType> GetAll<TEntityType>() where TEntityType : class
        {
            DbSet<TEntityType> dbSet = _dbContext.Set<TEntityType>();
            var entityList = dbSet.AsNoTracking();
            return entityList;
        }

        public IEnumerable<TEntityType> GetMany<TEntityType>(Expression<Func<TEntityType, bool>> whereClause) where TEntityType : class
        {
            DbSet<TEntityType> dbSet = _dbContext.Set<TEntityType>();
            IEnumerable<TEntityType> entityList = dbSet.Where(whereClause).AsNoTracking().ToList();
            return entityList;
        }

        public TEntityType GetSingleNoTrack<TEntityType>(Expression<Func<TEntityType, bool>> whereClause) where TEntityType : class
        {
            DbSet<TEntityType> dbSet = _dbContext.Set<TEntityType>();
            TEntityType entityObject = dbSet.Where(whereClause).AsNoTracking().SingleOrDefault();
            return entityObject;
        }

        public TEntityType GetSingleTracked<TEntityType>(Expression<Func<TEntityType, bool>> whereClause) where TEntityType : class
        {
            DbSet<TEntityType> dbSet = _dbContext.Set<TEntityType>();
            TEntityType entityObject = dbSet.Where(whereClause).SingleOrDefault();
            return entityObject;
        }
        #endregion Reads

        #region Create/Update/Delete
        public int InsertRecord<TEntityType>(TEntityType itemToInsert) where TEntityType : class
        {
            DbSet<TEntityType> dbSet = _dbContext.Set<TEntityType>();
            dbSet.Add(itemToInsert);
            var entriesWritten = _dbContext.SaveChanges();
            return entriesWritten;
        }

        public async Task<int> InsertRecordAsync<TEntityType>(TEntityType itemToInsert) where TEntityType : class
        {
            DbSet<TEntityType> dbSet = _dbContext.Set<TEntityType>();
            await dbSet.AddAsync(itemToInsert);
            int entriesWritten = await _dbContext.SaveChangesAsync();
            return entriesWritten;
        }

        public void InsertRecordList<TEntityType>(IEnumerable<TEntityType> listToInsert) where TEntityType : class
        {
            DbSet<TEntityType> dbSet = _dbContext.Set<TEntityType>();
            foreach (TEntityType listItem in listToInsert)
            {
                dbSet.Add(listItem);
            }
            _dbContext.SaveChanges();
        }

        public async Task InsertRecordListAsync<TEntityType>(IEnumerable<TEntityType> listToInsert) where TEntityType : class
        {
            DbSet<TEntityType> dbSet = _dbContext.Set<TEntityType>();
            foreach (TEntityType listItem in listToInsert)
            {
                dbSet.Add(listItem);
            }
            await _dbContext.SaveChangesAsync();
        }

        public void UpdateRecord<TEntityType>(TEntityType itemToUpdate) where TEntityType : class
        {
            _dbContext.Entry(itemToUpdate).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void UpdateRecordList<TEntityType>(IEnumerable<TEntityType> listToUpdate) where TEntityType : class
        {
            foreach (TEntityType listItem in listToUpdate)
            {
                _dbContext.Entry(listItem).State = EntityState.Modified;
            }
            _dbContext.SaveChanges();
        }

        public void DeleteRecord<TEntityType>(TEntityType itemToDelete) where TEntityType : class
        {
            _dbContext.Entry(itemToDelete).State = EntityState.Deleted;
            _dbContext.SaveChanges();
        }

        public void DeleteRecordById<TEntityType>(int primaryKeyId) where TEntityType : class
        {
            DbSet<TEntityType> dbSet = _dbContext.Set<TEntityType>();
            TEntityType entityObject = dbSet.Find(primaryKeyId);
            _dbContext.Entry(entityObject).State = EntityState.Deleted;
            _dbContext.SaveChanges();
        }

        public void DeleteRecordList<TEntityType>(IEnumerable<TEntityType> listToDelete) where TEntityType : class
        {
            foreach (TEntityType listItem in listToDelete)
            {
                _dbContext.Entry(listItem).State = EntityState.Deleted;
            }
            _dbContext.SaveChanges();
        }

        public void DeleteRecordListWithTransaction<TEntityType>(IEnumerable<TEntityType> listToDelete) where TEntityType : class
        {
            using (var dbContextTransaction = _dbContext.Database.BeginTransaction())
            {
                foreach (TEntityType listItem in listToDelete)
                {
                    _dbContext.Entry(listItem).State = EntityState.Deleted;
                }
                _dbContext.SaveChanges();
                dbContextTransaction.Commit();
            }
        }

        public DbSet<TEntityType> Set<TEntityType>() where TEntityType : class
        {
            throw new NotImplementedException();
        }
        #endregion Create/Update/Delete
    }
}
