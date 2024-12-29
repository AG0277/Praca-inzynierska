namespace Domain.Models
{
    public class AppUser : BaseObject
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; }
        public int AmountPaid { get; set; } = 0;
        public int AmountToBePaid { get; set; } = 0;
        public IEnumerable<RoomReservation> RoomReservations { get; set; } = new List<RoomReservation>();
        public IEnumerable<CourseUserAssigment> CourseUserAssigment { get; set; } = new List<CourseUserAssigment>();
    }
}
