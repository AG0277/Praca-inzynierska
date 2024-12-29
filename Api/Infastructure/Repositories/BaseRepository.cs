using Domain.Abstractions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infastructure.Repositories
{
    public abstract class BaseRepository<Entity> : IBaseRepository<Entity> where Entity : BaseObject
    {
        private readonly HostelDbContext _context;
        protected readonly DbSet<Entity> _dbSet;

        protected BaseRepository(HostelDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Entity>();
        }

        public async Task<Entity?> FindByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<Entity>> FindAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<Entity>> FindAsync(Expression<Func<Entity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<Entity> CreateAsync(Entity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> UpdateAsync(Entity entity)
        {
            var obj = _dbSet.Find(entity.Id);
            if (obj == null) return false;

            _dbSet.Entry(obj).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) return false;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
