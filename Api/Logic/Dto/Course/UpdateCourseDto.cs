namespace Logic.Dto.Course
{
    public class UpdateCourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string SeasonType { get; set; } = string.Empty;
        public int NumberOfDays { get; set; }
        public int Price { get; set; }
    }
}
