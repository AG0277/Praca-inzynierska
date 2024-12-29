using System.Collections.Generic;

namespace Domain.Models
{
    public class Course : BaseObject
    {
        public required string Name { get; set; } 
        public required string Description { get; set; }
        public required string SeasonType { get; set; }
        public int NumberOfDays { get; set; }
        public int Price { get; set; }
        public IEnumerable<CourseAssignment> CourseAssignments { get; set; } = new List<CourseAssignment>();
    }


    public static class SeasonType
    {
        public const string WINTER = "WINTER";
        public const string SUMMER = "SUMMER";
        public const string BOTH = "BOTH";
    }
}
