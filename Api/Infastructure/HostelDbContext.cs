using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infastructure
{
    public class HostelDbContext : IdentityDbContext<WorkerAccount>
    {
        public HostelDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomReservation> RoomReservations { get; set; }
        public DbSet<CourseAssignment> CourseAssignments { get; set; }
        public DbSet<CourseUserAssigment> CourseUserAssigments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Instructor> Instructors { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<AppUser>().HasKey(x => x.Id);
            builder.Entity<Room>().HasKey(x => x.Id);
            builder.Entity<RoomReservation>(x => x.HasKey(x => x.ReservationId));
            builder
                .Entity<RoomReservation>()
                .HasOne(a => a.AppUser)
                .WithMany(x => x.RoomReservations)
                .HasForeignKey(x => x.AppUserId);
            builder
                .Entity<RoomReservation>()
                .HasOne(r => r.Room)
                .WithMany(a => a.RoomReservations)
                .HasForeignKey(x => x.RoomId);
            builder
                .Entity<RoomReservation>()
                .Property(r => r.From)
                .HasColumnType("DATE");
            builder
                .Entity<RoomReservation>()
                .Property(r => r.To)
                .HasColumnType("DATE");


            builder.Entity<Course>().HasKey(x => x.Id);
            builder.Entity<Instructor>().HasKey(x => x.Id);
            builder.Entity<CourseAssignment>().HasKey(x => x.Id);
            builder
                .Entity<CourseAssignment>()
                .HasOne(x => x.Course)
                .WithMany(x => x.CourseAssignments)
                .HasForeignKey(x => x.CourseId);
            builder
                .Entity<CourseAssignment>()
                .HasOne(x => x.Instructor)
                .WithMany(x => x.CourseAssignments)
                .HasForeignKey(x => x.InstructorId);

            builder
                .Entity<CourseAssignment>()
                .Property(r => r.StartDate)
                .HasColumnType("DATE");
            builder
                .Entity<CourseAssignment>()
                .Property(r => r.EndDate)
                .HasColumnType("DATE");

            builder.Entity<CourseUserAssigment>().HasKey(x => new { x.CourseAssigmentId, x.AppUserId });
            builder
                .Entity<CourseUserAssigment>()
                .HasOne(x => x.CourseAssignment)
                .WithMany(x => x.CourseUserAssigment)
                .HasForeignKey(x => new { x.CourseAssigmentId });
            builder
                .Entity<CourseUserAssigment>()
                .HasOne(x => x.AppUser)
                .WithMany(x => x.CourseUserAssigment)
                .HasForeignKey(x => x.AppUserId);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Name = "User", NormalizedName = "USER" }
            };
            builder.Entity<IdentityRole>().HasData(roles);

        }
    }
}
