
using Chilaqueria_API.Datos;
using Microsoft.EntityFrameworkCore;

namespace Chilaqueria_API.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }

    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ChilaqueriaDBContext _context;
        private readonly DbSet<T> dbSet;

        public Repository(ChilaqueriaDBContext chilaqueriaDB)
        {
            _context = chilaqueriaDB;
            dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var r = await dbSet.ToListAsync();
            return r;
        }

        public async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);

        }

        public async Task<T> GetByIdAsync(int id)
        {
            var f = await dbSet.FindAsync(id);
            return f;
        }

        public async Task UpdateAsync(T entity)
        {
            dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
    }

}
