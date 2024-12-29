using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Dto.CourseUserAssigment
{
    public class CreateCourseUserAssigmentDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; }
        public int NumberOfPeopleSigningUp { get; set; }
        public int CourseAssigmentId { get; set; }
        public int AmountPaid { get; set; }
        public int AmountToBePaid { get; set; }
    }
}
