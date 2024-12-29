namespace Domain.Models
{
    public class Instructor : BaseObject
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public IEnumerable<CourseAssignment> CourseAssignments { get; set; } = new List<CourseAssignment>();
    }
}
