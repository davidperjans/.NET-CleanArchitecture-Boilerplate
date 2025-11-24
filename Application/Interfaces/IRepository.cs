using Application.Common;
using System.Linq.Expressions;

namespace Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<OperationResult<T>> GetByIdAsync(int id);
        Task<OperationResult<IEnumerable<T>>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<OperationResult<T>> AddAsync(T entity);
        Task<OperationResult<T>> UpdateAsync(T entity);
        Task<OperationResult<bool>> DeleteAsync(int id);
    }
}
