using Domain.Models;
using Logic.Logic.Abstractions;

namespace Logic.Logic.Abstractions
{
    public interface ICourseAssigmentLogic: ILogic
    {
        public Task<CourseAssignment?> GetById(int courseAssigemtnId);
        public Task<List<CourseAssignment>> GetAll();

        public Task<CourseAssignment> Create(CourseAssignment courseAssigmentDto);
        public Task<bool> Update(CourseAssignment courseAssigment);
        public Task<bool> Delete(int id);

    }
}
