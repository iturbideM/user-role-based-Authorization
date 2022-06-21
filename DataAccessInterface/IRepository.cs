using System.Linq.Expressions;

namespace DataAccessInterface
{
    public interface IRepository<T> where T : class
    {
        T Get(Expression<Func<T, bool>> expression);
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? expression = null);
        T Add(T entity);
        bool Exists(Expression<Func<T, bool>> expression);
        T Update(T update);
        void Delete(object id);
    }
}