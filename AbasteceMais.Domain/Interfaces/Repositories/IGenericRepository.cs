using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AbasteceMais.Domain.Interfaces.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> QueryableObject();

        TEntity Get(Expression<Func<TEntity, bool>> condition, params Expression<Func<TEntity, object>>[] includes);

        IList<TEntity> GetAll(Expression<Func<TEntity, bool>> condition, params Expression<Func<TEntity, object>>[] includes);

        void Insert(TEntity obj);

        void InsertAll(IList<TEntity> objs);

        void Update(TEntity obj);

        void UpdateAll(IList<TEntity> objs);

        void Delete(TEntity obj);

        void DeleteAll(IList<TEntity> objs);

        void DeleteAll(Expression<Func<TEntity, bool>> condition);

        int RecordCount(Expression<Func<TEntity, bool>> condition);

        bool RecordExists(Expression<Func<TEntity, bool>> condition);
    }
}
