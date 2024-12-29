using Domain.Abstractions;
using Domain.Models;

namespace Infastructure.Repositories
{
    internal sealed class InstructorRepository : BaseRepository<Instructor>, IInstructorRepository
    {
        public InstructorRepository(HostelDbContext hostelDbContext) : base(hostelDbContext)
        {

        }
    }
}
