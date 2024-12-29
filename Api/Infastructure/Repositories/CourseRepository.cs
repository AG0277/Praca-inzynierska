using Domain.Abstractions;
using Domain.Models;

namespace Infastructure.Repositories
{
    internal sealed class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        public CourseRepository(HostelDbContext context) : base(context)
        {

        }
    }
}
