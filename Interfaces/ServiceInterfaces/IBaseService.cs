using System.Linq.Expressions;

namespace OrderManagementSystem.Interfaces.ServiceRepositories
{
    public interface IBaseService<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T?> GetById(int id);
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveChanges();
    }
}
