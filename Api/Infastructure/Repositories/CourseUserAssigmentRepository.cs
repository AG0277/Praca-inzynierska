using Domain.Abstractions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infastructure.Repositories
{
    internal class CourseUserAssigmentRepository : ICourseUserAssigmentRepository
    {
        private readonly HostelDbContext _dbContext;
        private readonly DbSet<CourseUserAssigment> _dbSet;

        public CourseUserAssigmentRepository(HostelDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<CourseUserAssigment>();
        }
        public async Task<CourseUserAssigment> CreateAsync(CourseUserAssigment courseUserAssigment)
        {
            await _dbSet.AddAsync(courseUserAssigment);
            await _dbContext.SaveChangesAsync();
            return courseUserAssigment;
        }

        public async Task<bool> DeleteAsync(int appUserId, int courseAssigmentId)
        {
            var res = await _dbSet.FirstOrDefaultAsync(x => x.CourseAssigmentId == courseAssigmentId && x.AppUserId == appUserId);
            if (res == null) return false;
            _dbSet.Remove(res);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(CourseUserAssigment courseUserAssigment)
        {
            var res = await _dbSet.FirstOrDefaultAsync(x => x.CourseAssigmentId == courseUserAssigment.CourseAssigmentId && x.AppUserId == courseUserAssigment.AppUserId);
            if (res == null) return false;

            _dbSet.Entry(res).CurrentValues.SetValues(courseUserAssigment);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<CourseUserAssigment?> FindByIdAsync(int appUserId, int courseAssigmentId)
        {
           return await _dbSet
                .Include(x=>x.CourseAssignment)
                .Include(x=> x.AppUser)
                .FirstOrDefaultAsync(x=>x.AppUserId == appUserId && x.CourseAssigmentId == courseAssigmentId);
        }
    }
}
