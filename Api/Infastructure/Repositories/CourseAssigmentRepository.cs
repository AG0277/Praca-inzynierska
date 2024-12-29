using Domain.Abstractions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infastructure.Repositories
{
    internal sealed class CourseAssigmentRepository : ICourseAssigmentRepository
    {
        private readonly HostelDbContext _dbContext;
        private readonly DbSet<CourseAssignment> _dbSet;

        public CourseAssigmentRepository(HostelDbContext hostelDbContext)
        {
            _dbContext = hostelDbContext;
            _dbSet = _dbContext.Set<CourseAssignment>();
        }

        public async Task<CourseAssignment?> GetById(int courseAssigemtnId)
        {
            return await _dbSet
                .Include(x => x.Course)
                .Include(x => x.Instructor)
                .FirstOrDefaultAsync(x => x.Id == courseAssigemtnId);
        }

        public async Task<List<CourseAssignment>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<CourseAssignment> Create(CourseAssignment course)
        {
            await _dbSet.AddAsync(course);
            await _dbContext.SaveChangesAsync();
            return course;
        }

        public async Task<bool> Update(CourseAssignment course)
        {
            var res = await _dbSet.FirstOrDefaultAsync(x=>x.Id == course.Id);
            if (res == null) return false;

            _dbSet.Entry(res).CurrentValues.SetValues(course);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var res = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (res == null) return false;

            _dbSet.Remove(res);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
