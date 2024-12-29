using api.Models;

namespace api.Repositories.Abstractions
{
    public interface ICourseAssigmentRepository: IRepository
    {
        public Task<CourseAssignment?> GetByIds(int courseId, int instructorId);

        public Task<CourseAssignment> Create(CourseAssignment course);

        public Task<bool> Update(CourseAssignment course);
    }
}
