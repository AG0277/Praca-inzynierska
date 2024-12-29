namespace Logic.Dto.CourseAssigment
{
    public class CreateCourseAssigmentDto
    {
        public int CourseId { get; set; }
        public int InstructorId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
