using System.Linq.Expressions;
using DataAccessInterface;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.SqlServer
{
    public class SqlServerRepository<T> : IRepository<T> where T : class
    {
        private DbContext _context;
        private DbSet<T> _set;

        public SqlServerRepository(DbContext context)
        {
            this._context = context;
            this._set = context.Set<T>();
        }

        public T Get(Expression<Func<T, bool>> expression)
        {
            return this._set.FirstOrDefault(expression);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> expression = null)
        {
            if (expression != null)
                return this._set.Where(expression);
            else
                return this._set.ToList();
        }

        public T Add(T entity)
        {
            this._set.Add(entity);
            this._context.SaveChanges();
            return entity;
        }

        public bool Exists(Expression<Func<T, bool>> expression)
        {
            return this._set.Any(expression);
        }

        public T Update(T update)
        {
            _set.Attach(update);
            _context.Entry(update).State = EntityState.Modified;
            _context.SaveChanges();

            return update;
        }

        public void Delete(object id)
        {
            T existing = _set.Find(id);
            _set.Remove(existing);
            _context.SaveChanges();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

    }
}