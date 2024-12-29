using Domain.Models;

namespace Domain.Abstractions
{
    public interface ICourseAssigmentRepository: IRepository
    {
        public Task<CourseAssignment?> GetById(int courseAssigemtnId);
        public Task<List<CourseAssignment>> GetAll();

        public Task<CourseAssignment> Create(CourseAssignment course);

        public Task<bool> Update(CourseAssignment course);
        public Task<bool> Delete(int id);
    }
}
