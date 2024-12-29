namespace Domain.Models
{
    public class CourseAssignment: BaseObject
    {
        public int CourseId { get; set; }
        public int InstructorId { get; set; }
        public int? BaseCourseCost { get; set; } // dla zmiennej ceny
        public int? BaseCourseCostPerPerson { get; set; } // dla stalej ceny
        public int? CurrentlySigned { get; set; }
        public int? MaxPeople { get; set; }
        public bool? PrivateSession { get; set; }
        public bool? AdjustingPriceForPrivateSession { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public required Course Course { get; set; }
        public required Instructor Instructor { get; set; }
        public IEnumerable<CourseUserAssigment> CourseUserAssigment { get; set; } = new List<CourseUserAssigment>();
    }
}
