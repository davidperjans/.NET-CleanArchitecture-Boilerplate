using Application.Common;
using Application.Interfaces;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public AppDbContext _context;
        private readonly DbSet<T> _dbSet;
        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public async Task<OperationResult<T>> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return OperationResult<T>.Success(entity);
        }

        public async Task<OperationResult<bool>> DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                return OperationResult<bool>.Failure("Entity not found");

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return OperationResult<bool>.Success(true);
        }

        public async Task<OperationResult<IEnumerable<T>>> GetAllAsync()
        {
            var list = await _dbSet.ToListAsync();
            return OperationResult<IEnumerable<T>>.Success(list);
        }

        public async Task<OperationResult<T>> GetByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            return entity != null
                ? OperationResult<T>.Success(entity)
                : OperationResult<T>.Failure("Entity not found");
        }

        public async Task<OperationResult<T>> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return OperationResult<T>.Success(entity);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }
    }
}
