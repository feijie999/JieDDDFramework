using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using JieDDDFramework.Core.Domain;

namespace JieDDDFramework.Data.Repository
{
    public interface IRepositoryBase : IDisposable
    {
        IRepositoryBase BeginTrans(IsolationLevel isolationLevel = IsolationLevel.RepeatableRead);

        int Commit();

        int Insert<TEntity>(TEntity entity) where TEntity : Entity;

        int Insert<TEntity>(List<TEntity> entities) where TEntity : Entity;

        int Update<TEntity>(TEntity entity) where TEntity : Entity;

        int Delete<TEntity>(TEntity entity) where TEntity : Entity;

        int Delete<TEntity>(Expression<Func<TEntity, bool>> criterion) where TEntity : Entity;

        TEntity FindEntity<TEntity>(object keyValue) where TEntity : Entity;

        TEntity FindEntity<TEntity>(Expression<Func<TEntity, bool>> criterion) where TEntity : Entity;

        IQueryable<TEntity> Tables<TEntity>() where TEntity : Entity;

        IQueryable<TEntity> Tables<TEntity>(Expression<Func<TEntity, bool>> criterion)
            where TEntity : Entity;
    }
}
