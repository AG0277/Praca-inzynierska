using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Dto.CourseAssigment
{
    public class GetCourseAssigmentDto
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int InstructorId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
