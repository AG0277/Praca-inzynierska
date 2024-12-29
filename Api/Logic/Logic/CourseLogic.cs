using Domain.Abstractions;
using Domain.Models;
using Logic.Logic.Abstractions;
using Logic.Logic.Base;

namespace Logic.Logic
{
    public sealed class CourseLogic : BaseLogic<Course>, ICourseLogic
    {
        public CourseLogic(ICourseRepository courseRepository) : base(courseRepository)
        {

        }
    }
}
