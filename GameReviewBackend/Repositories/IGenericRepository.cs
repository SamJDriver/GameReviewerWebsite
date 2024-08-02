using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public interface IGenericRepository<TContext> where TContext : DbContext
{
    DbSet<TEntityType> Set<TEntityType>() where TEntityType : class;
    TEntityType GetById<TEntityType>(int id) where TEntityType : class;
    IQueryable<TEntityType> GetAll<TEntityType>() where TEntityType : class;
    IEnumerable<TEntityType> GetMany<TEntityType>(Expression<Func<TEntityType, bool>> whereClause) where TEntityType : class;
    TEntityType GetSingleNoTrack<TEntityType>(Expression<Func<TEntityType, bool>> whereClause) where TEntityType : class;
    TEntityType GetSingleTracked<TEntityType>(Expression<Func<TEntityType, bool>> whereClause) where TEntityType : class;
    int InsertRecord<TEntityType>(TEntityType itemToInsert) where TEntityType : class;
    Task<int> InsertRecordAsync<TEntityType>(TEntityType itemToInsert) where TEntityType : class;
    void InsertRecordList<TEntityType>(IEnumerable<TEntityType> listToInsert) where TEntityType : class;
    Task InsertRecordListAsync<TEntityType>(IEnumerable<TEntityType> listToInsert) where TEntityType : class;
    void UpdateRecord<TEntityType>(TEntityType itemToUpdate) where TEntityType : class;
    void UpdateRecordList<TEntityType>(IEnumerable<TEntityType> listToUpdate) where TEntityType : class;
    void DeleteRecord<TEntityType>(TEntityType itemToDelete) where TEntityType : class;
    void DeleteRecordById<TEntityType>(int primaryKeyId) where TEntityType : class;
    void DeleteRecordList<TEntityType>(IEnumerable<TEntityType> listToDelete) where TEntityType : class;
    void DeleteRecordListWithTransaction<TEntityType>(IEnumerable<TEntityType> listToDelete) where TEntityType : class;
}
