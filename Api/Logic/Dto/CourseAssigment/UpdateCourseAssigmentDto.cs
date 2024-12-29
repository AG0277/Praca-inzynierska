namespace Logic.Dto.CourseAssigment
{
    public class UpdateCourseAssigmentDto
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int InstructorId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
