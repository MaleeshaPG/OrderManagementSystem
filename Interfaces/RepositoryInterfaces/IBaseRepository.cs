using System.Linq.Expressions;

namespace OrderManagementSystem.Interfaces.RepositoryInterfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T?> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveChanges();
    }
}
