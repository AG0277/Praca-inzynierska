namespace Domain.Models
{
    public class CourseUserAssigment
    {
        public int CourseAssigmentId { get; set; }
        public int AppUserId { get; set; }
        public required CourseAssignment CourseAssignment { get; set; }
        public required AppUser AppUser { get; set; }
        public int NumberOfPeopleSigningUp { get; set; }
        public int AmountPaid { get; set; }
        public int AmountToBePaid { get; set; }
    }
}
