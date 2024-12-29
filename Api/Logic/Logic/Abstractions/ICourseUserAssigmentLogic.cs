using Domain.Abstractions;
using Domain.Models;
using Logic.Dto.CourseUserAssigment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Logic.Abstractions
{
    public interface ICourseUserAssigmentLogic : ILogic
    {
        public Task<CourseUserAssigment?> GetByIds(int appUserId, int courseAssigmentId);

        public Task<CourseUserAssigment> Create(CreateCourseUserAssigmentDto course);

        public Task<bool> Update(CourseUserAssigment course);
        public Task<bool> Delete(int appUserId, int courseAssigmentId);
    }
}
