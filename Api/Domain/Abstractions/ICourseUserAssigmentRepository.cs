using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions
{
    public interface ICourseUserAssigmentRepository: IRepository
    {
        public Task<CourseUserAssigment?> FindByIdAsync(int appUserId, int courseAssigmentId);
        public Task<CourseUserAssigment> CreateAsync(CourseUserAssigment courseUserAssigment);
        public Task<bool> UpdateAsync(CourseUserAssigment courseUserAssigment);
        public Task<bool> DeleteAsync(int appUserId, int courseAssigmentId);
    }
}
