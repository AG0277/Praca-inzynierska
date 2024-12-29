using Domain.Abstractions;
using Domain.Models;
using Logic.Logic.Abstractions;
using Logic.Logic.Base;

namespace Logic.Logic
{
    public class InstructorLogic : BaseLogic<Instructor>, IInstructorLogic
    {
        private readonly IInstructorRepository _instructorRepository;

        public InstructorLogic(IInstructorRepository instructorRepository) : base(instructorRepository)
        {
            _instructorRepository = instructorRepository;
        }
    }
}
