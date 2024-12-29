using Domain.Abstractions;
using Domain.Models;
using Logic.Logic.Abstractions;

namespace Logic.Logic
{
    public sealed class CourseAssigmentLogic: ICourseAssigmentLogic
    {
        private readonly ICourseAssigmentRepository _courseAssigmentRepository;

        public CourseAssigmentLogic(ICourseAssigmentRepository courseAssigmentRepository)
        {
            _courseAssigmentRepository = courseAssigmentRepository;
        }
        public async  Task<CourseAssignment?> GetById(int courseAssigemtnId)
        {
            return await _courseAssigmentRepository.GetById(courseAssigemtnId);
        }

        public async  Task<List<CourseAssignment>> GetAll()
        {
            return await  _courseAssigmentRepository.GetAll();
        }

        public async  Task<CourseAssignment> Create(CourseAssignment course)
        {
            return await _courseAssigmentRepository.Create(course);
        }

        public async  Task<bool> Update(CourseAssignment course)
        {
            return await _courseAssigmentRepository.Update(course);
        }

        public async Task<bool> Delete(int id)
        {
            return await _courseAssigmentRepository.Delete(id);
        }
    }
}
