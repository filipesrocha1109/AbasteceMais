using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using AbasteceMais.Data.Context;
using AbasteceMais.Domain.Interfaces.Repositories;

namespace AbasteceMais.Data.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public IQueryable<TEntity> QueryableObject()
        {
            return _dbSet.AsNoTracking();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> condition, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> dbQuery = _dbSet;

            foreach (var include in includes)
                dbQuery = dbQuery.Include(include);

            return dbQuery.SingleOrDefault(condition);
        }

        public IList<TEntity> GetAll(Expression<Func<TEntity, bool>> condition, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> dbQuery = _dbSet;

            foreach (var include in includes)
                dbQuery = dbQuery.Include(include);

            return dbQuery.Where(condition).ToList();
        }

        public void Insert(TEntity obj)
        {
            _dbSet.Add(obj);
        }

        public void InsertAll(IList<TEntity> objs)
        {
            foreach (var item in objs)
                _dbSet.Add(item);
        }

        public void Update(TEntity obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
        }

        public void UpdateAll(IList<TEntity> objs)
        {
            foreach (var item in objs)
                _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(TEntity obj)
        {
            _dbSet.Remove(obj);
        }

        public void DeleteAll(IList<TEntity> objs)
        {
            _dbSet.RemoveRange(objs);
        }

        public void DeleteAll(Expression<Func<TEntity, bool>> condition)
        {
            _dbSet.RemoveRange(_context.Set<TEntity>().Where(condition).AsEnumerable());
        }

        public int RecordCount(Expression<Func<TEntity, bool>> condition)
        {
            return _dbSet.AsNoTracking().Where(condition).Count();
        }

        public bool RecordExists(Expression<Func<TEntity, bool>> condition)
        {
            return _dbSet.AsNoTracking().Where(condition).Any();
        }
    }
}
