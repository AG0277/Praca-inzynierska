using AutoMapper;
using Domain.Models;
using Logic.Dto.AppUser;
using Logic.Dto.Course;
using Logic.Dto.CourseAssigment;
using Logic.Dto.Instructor;
using Logic.Dto.RoomDtos;
using Logic.Dto.RoomReservation;
using Logic.Dto.RoomReservationDto;

namespace api.AutoMapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateAppUserDto, AppUser>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.AmountToBePaid, opt => opt.Ignore())
                .ForMember(dest => dest.AmountPaid, opt => opt.Ignore())
                .ForMember(dest => dest.CourseUserAssigment, opt => opt.Ignore())
                .ForMember(dest => dest.RoomReservations, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<AppUser, UpdateAppUserDto>()
                .ReverseMap();

            
            CreateMap<Room, CreateRoomDto>().ReverseMap()
                .ForMember(dest => dest.RoomReservations, opt => opt.Ignore());
            CreateMap<Room, UpdateRoomDto>().ReverseMap()
                .ForMember(dest => dest.RoomReservations, opt => opt.Ignore());
            CreateMap<Room, GetAllRoomsDto>().ReverseMap()
                .ForMember(dest => dest.RoomReservations, opt => opt.Ignore())
                .ForMember(dest => dest.NumberOfReservedBeds, opt => opt.Ignore());


            CreateMap<Course, CreateCourseDto>().ReverseMap()
                .ForMember(dest => dest.CourseAssignments, opt => opt.Ignore());
            CreateMap<Course, UpdateCourseDto>().ReverseMap()
                .ForMember(dest => dest.CourseAssignments, opt => opt.Ignore());


            CreateMap<Instructor, CreateInstructorDto>().ReverseMap()
                .ForMember(dest => dest.CourseAssignments, opt => opt.Ignore());


            CreateMap<CreateRoomReservationDto, RoomReservation>()
                .ForMember(dest => dest.AppUser, opt => opt.Ignore())
                .ForMember(dest => dest.Room, opt => opt.Ignore());

            CreateMap<RoomReservation, CreateRoomReservationDto>().ReverseMap();


            CreateMap<CreateCourseAssigmentDto, CourseAssignment>()
                .ForMember(dest => dest.Course, opt => opt.Ignore())
                .ForMember(dest => dest.CourseUserAssigment, opt => opt.Ignore())
                .ForMember(dest => dest.Instructor, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UpdateCourseAssigmentDto, CourseAssignment>()
    .ForMember(dest => dest.Course, opt => opt.Ignore())
    .ForMember(dest => dest.CourseUserAssigment, opt => opt.Ignore())
    .ForMember(dest => dest.Instructor, opt => opt.Ignore())
    .ReverseMap();


            CreateMap<GetCourseAssigmentDto, CourseAssignment>()
                .ForMember(dest => dest.Course, opt => opt.Ignore())
                .ForMember(dest => dest.CourseUserAssigment, opt => opt.Ignore())
                .ForMember(dest => dest.Instructor, opt => opt.Ignore())
                .ReverseMap();


            CreateMap<GetRoomReservationDto, RoomReservation>()
                .ForMember(dest => dest.Room, opt => opt.Ignore())
                .ForMember(dest => dest.AppUser, opt => opt.Ignore())
                .ReverseMap();

        }
    }
}
