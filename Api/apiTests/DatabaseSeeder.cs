using Domain.Models;
using Infastructure;

namespace apiTests
{
    internal static class DatabaseSeeder
    {
        public static async void Seed(HostelDbContext hostelDbContext)
        {
            var appUsers = new List<AppUser>
            {
                new AppUser { FirstName = "Mariusz",Email ="Mariusz@gmail.com",  LastName = "Pudzianowski", PhoneNumber = "1234567890", AmountPaid = 740, AmountToBePaid = 3000},
                new AppUser { FirstName = "Robert",Email ="Robert@gmail.com", LastName = "Kubica", PhoneNumber = "0987654321", AmountPaid = 840, AmountToBePaid = 6720 },
                new AppUser { FirstName = "Justyna", Email = "Justyna@gmail.com", LastName = "Kowalczyk", PhoneNumber = "1122334455", AmountPaid = 140, AmountToBePaid = 900 }
            };

            var rooms = new List<Room>
            {
                new Room { RoomNumber = 101, NumberOfBeds = 5, NumberOfReservedBeds = 3, PricePerBed = 100 },
                new Room { RoomNumber = 102, NumberOfBeds = 6, NumberOfReservedBeds = 6, PricePerBed = 120 },
                new Room { RoomNumber = 103, NumberOfBeds = 7, NumberOfReservedBeds = 0, PricePerBed = 80 },
                new Room { RoomNumber = 104, NumberOfBeds = 9, NumberOfReservedBeds = 2, PricePerBed = 150 },
                new Room { RoomNumber = 105, NumberOfBeds = 9, NumberOfReservedBeds = 0, PricePerBed = 90 }
            };
            await hostelDbContext.AppUsers.AddRangeAsync(appUsers);
            await hostelDbContext.Rooms.AddRangeAsync(rooms);
            await hostelDbContext.SaveChangesAsync();

            var roomReservations = new List<RoomReservation>
            {
                new RoomReservation {  AppUserId = 1, RoomId = 1, NumberOfBeds = 3, From = DateTime.UtcNow.AddDays(1), To = DateTime.UtcNow.AddDays(4), AmountPaid = 120, TotalPrice = 900, AppUser = appUsers[0], Room = rooms[0] },
                new RoomReservation {  AppUserId = 2, RoomId = 4, NumberOfBeds = 6, From = DateTime.UtcNow.AddDays(3), To = DateTime.UtcNow.AddDays(4), AmountPaid = 240, TotalPrice = 720, AppUser = appUsers[1], Room = rooms[3] },
                new RoomReservation {  AppUserId = 3, RoomId = 4, NumberOfBeds = 2, From = DateTime.UtcNow.AddDays(2), To = DateTime.UtcNow.AddDays(3), AmountPaid = 150, TotalPrice = 150, AppUser = appUsers[2], Room = rooms[3] }
            };


            await hostelDbContext.RoomReservations.AddRangeAsync(roomReservations);
            await hostelDbContext.SaveChangesAsync();

            var courses = new List<Course>
            {
                new Course { Name = "Hiking Both Seasons Course", Description = "A hiking course for all seasons.", SeasonType = SeasonType.BOTH, NumberOfDays = 2 },
                new Course { Name = "Mountaineering Both Seasons Course", Description = "A mountaineering course for all seasons.",  SeasonType = SeasonType.BOTH, NumberOfDays = 10 },
                new Course { Name = "Rock Climbing Summer Course", Description = "A rock climbing course for summer season.", SeasonType = SeasonType.SUMMER, NumberOfDays = 3 },
                new Course { Name = "Ice Climbing Winter Course", Description = "An ice climbing course for winter season.", SeasonType = SeasonType.WINTER, NumberOfDays = 4 },
                new Course { Name = "Via Ferrata Summer Course", Description = "A via ferrata course for summer season.",  SeasonType = SeasonType.SUMMER, NumberOfDays = 2 },
                new Course { Name = "Ski Mountaineering Winter Course", Description = "A ski mountaineering course for winter season.",  SeasonType = SeasonType.WINTER, NumberOfDays = 6 },
                new Course { Name = "Rescue and Safety Both Seasons Course", Description = "A rescue and safety course for all seasons.",  SeasonType = SeasonType.BOTH, NumberOfDays = 4 },
            };

            var instructors = new List<Instructor>
            {
                new Instructor { FirstName = "Robert", LastName = "Lewandowski", PhoneNumber = "+48 601 123 456"},
                new Instructor { FirstName = "Iga", LastName = "Świątek", PhoneNumber = "+48 602 654 321"},  
                new Instructor { FirstName = "Adam", LastName = "Małysz", PhoneNumber = "+48 603 987 654"},    
                new Instructor { FirstName = "Anita", LastName = "Włodarczyk", PhoneNumber = "+48 604 321 987"},
                new Instructor { FirstName = "Zbigniew", LastName = "Bródka", PhoneNumber = "+48 605 555 111" },   
                new Instructor { FirstName = "Kamil", LastName = "Stoch", PhoneNumber = "+48 607 444 555"}      
            };

            await hostelDbContext.Instructors.AddRangeAsync(instructors);
            await hostelDbContext.Courses.AddRangeAsync(courses);
            await hostelDbContext.SaveChangesAsync();

            var courseAssignments = new List<CourseAssignment>
            {
                new CourseAssignment { CourseId = 1, InstructorId = 1, BaseCourseCost = 150, BaseCourseCostPerPerson = 500, CurrentlySigned = 10, MaxPeople = 15, PrivateSession = false, AdjustingPriceForPrivateSession = false, StartDate = DateTime.UtcNow.AddDays(3), EndDate = DateTime.UtcNow.AddDays(5), Course = courses[0], Instructor = instructors[0] }, // "Hiking Both Seasons Course" - Robert Lewandowski
                new CourseAssignment { CourseId = 2, InstructorId = 2, BaseCourseCost = 10000, BaseCourseCostPerPerson = 1000, CurrentlySigned = 8, MaxPeople = 12, PrivateSession = false, AdjustingPriceForPrivateSession = false, StartDate = DateTime.UtcNow.AddDays(4), EndDate = DateTime.UtcNow.AddDays(14), Course = courses[1], Instructor = instructors[1] }, // "Mountaineering Both Seasons Course" - Iga Świątek
                new CourseAssignment { CourseId = 3, InstructorId = 3, BaseCourseCost = 3000, BaseCourseCostPerPerson = 300, CurrentlySigned = 6, MaxPeople = 10, PrivateSession = false, AdjustingPriceForPrivateSession = false, StartDate = DateTime.UtcNow.AddDays(6), EndDate = DateTime.UtcNow.AddDays(9), Course = courses[2], Instructor = instructors[2] }, // "Rock Climbing Summer Course" - Adam Małysz
                new CourseAssignment { CourseId = 4, InstructorId = 4, BaseCourseCost = 4000, BaseCourseCostPerPerson = 400, CurrentlySigned = 5, MaxPeople = 8, PrivateSession = false, AdjustingPriceForPrivateSession = false, StartDate = DateTime.UtcNow.AddDays(7), EndDate = DateTime.UtcNow.AddDays(11), Course = courses[3], Instructor = instructors[3] }, // "Ice Climbing Winter Course" - Anita Włodarczyk
                new CourseAssignment { CourseId = 5, InstructorId = 5, BaseCourseCost = 2000, BaseCourseCostPerPerson = 200, CurrentlySigned = 8, MaxPeople = 12, PrivateSession = false, AdjustingPriceForPrivateSession = false, StartDate = DateTime.UtcNow.AddDays(10), EndDate = DateTime.UtcNow.AddDays(12), Course = courses[4], Instructor = instructors[4] }, // "Via Ferrata Summer Course" - Zbigniew Bródka
                new CourseAssignment { CourseId = 6, InstructorId = 6, BaseCourseCost = 6000, BaseCourseCostPerPerson = 600, CurrentlySigned = 4, MaxPeople = 6, PrivateSession = false, AdjustingPriceForPrivateSession = false, StartDate = DateTime.UtcNow.AddDays(11), EndDate = DateTime.UtcNow.AddDays(15), Course = courses[5], Instructor = instructors[5] }, // "Ski Mountaineering Winter Course" - Kamil Stoch
                new CourseAssignment { CourseId = 7, InstructorId = 1, BaseCourseCost = 4000, BaseCourseCostPerPerson = 400, CurrentlySigned = 12, MaxPeople = 15, PrivateSession = false, AdjustingPriceForPrivateSession = false, StartDate = DateTime.UtcNow.AddDays(13), EndDate = DateTime.UtcNow.AddDays(17), Course = courses[6], Instructor = instructors[0] }  // "Rescue and Safety Both Seasons Course" - Robert Lewandowski
            };


            await hostelDbContext.CourseAssignments.AddRangeAsync(courseAssignments);
            await hostelDbContext.SaveChangesAsync();

            var courseUserAssigments = new List<CourseUserAssigment>
            {
                new CourseUserAssigment { AppUserId = 1, CourseAssigmentId = 1, CourseAssignment = courseAssignments[0], AppUser = appUsers[0], NumberOfPeopleSigningUp = 3, AmountPaid = 500, AmountToBePaid = 1500 }, // Mariusz Pudzianowski -> "Hiking Both Seasons Course" - Robert Lewandowski
                new CourseUserAssigment { AppUserId = 2, CourseAssigmentId = 2, CourseAssignment = courseAssignments[1], AppUser = appUsers[1], NumberOfPeopleSigningUp = 6, AmountPaid = 600, AmountToBePaid = 6000 }, // Robert Kubica -> "Mountaineering Both Seasons Course" - Iga Świątek
                new CourseUserAssigment { AppUserId = 3, CourseAssigmentId = 3, CourseAssignment = courseAssignments[2], AppUser = appUsers[2], NumberOfPeopleSigningUp = 2, AmountPaid = 60, AmountToBePaid = 600 }, // Justyna Kowalczyk -> "Rock Climbing Summer Course" - Adam Małysz
                new CourseUserAssigment { AppUserId = 1, CourseAssigmentId = 4, CourseAssignment = courseAssignments[3], AppUser = appUsers[0], NumberOfPeopleSigningUp = 3, AmountPaid = 120, AmountToBePaid = 1200 }, // Mariusz Pudzianowski -> "Ice Climbing Winter Course" - Anita Włodarczyk
            };

            await hostelDbContext.CourseUserAssigments.AddRangeAsync(courseUserAssigments);
            await hostelDbContext.SaveChangesAsync();

        }
    }
}
