using OrderManagementSystem.Interfaces.RepositoryInterfaces;
using OrderManagementSystem.Interfaces.ServiceRepositories;
using System.Linq.Expressions;

namespace OrderManagementSystem.Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        protected readonly IBaseRepository<T> _repository;

        public BaseService(IBaseRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<T?> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await _repository.Find(predicate);
        }

        public async Task Add(T entity)
        {
            await _repository.Add(entity);
        }

        public void Update(T entity)
        {
            _repository.Update(entity);
        }

        public void Delete(T entity)
        {
            _repository.Delete(entity);
        }

        public async Task SaveChanges()
        {
            await _repository.SaveChanges();
        }
    }
}
