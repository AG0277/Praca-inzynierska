using Domain.Abstractions;
using Domain.Models;
using Logic.Dto.CourseUserAssigment;
using Logic.Logic.Abstractions;

namespace Logic.Logic
{
    internal class CourseUserAssigmentLogic : ICourseUserAssigmentLogic
    {
        private readonly ICourseUserAssigmentRepository _courseUserAssigmentRepository;
        private readonly ICourseAssigmentLogic _courseAssigmentLogic;
        private readonly IAppUserLogic _appUserLogic;

        public CourseUserAssigmentLogic(ICourseUserAssigmentRepository courseUserAssigmentRepository, ICourseAssigmentLogic courseAssigmentLogic, IAppUserLogic appUserLogic)
        {
            _courseUserAssigmentRepository =  courseUserAssigmentRepository;
            _courseAssigmentLogic = courseAssigmentLogic;
            _appUserLogic = appUserLogic;
        }
        public Task<CourseUserAssigment?> GetByIds(int appUserId , int courseAssigmentId)
        {
            return _courseUserAssigmentRepository.FindByIdAsync(appUserId, courseAssigmentId);
        }

        public async Task<CourseUserAssigment> Create(CreateCourseUserAssigmentDto courseDTO)
        {
            var courseInstructor = await _courseAssigmentLogic.GetById(courseDTO.CourseAssigmentId);
            if (courseInstructor == null) { throw new Exception("Nie znaleziono kursu"); };

            var user = await _appUserLogic.GetByName(courseDTO.FirstName, courseDTO.LastName);
            if (user == null)
            {
                user = await _appUserLogic.GetByPhoneNumber(courseDTO.PhoneNumber);
                if (user == null) 
                {
                    user = new AppUser() { FirstName = courseDTO.FirstName, LastName = courseDTO.LastName, PhoneNumber = courseDTO.PhoneNumber, Email = courseDTO.Email};
                    await _appUserLogic.AddAsync(user);
                }
            };

            user.AmountPaid += courseDTO.AmountPaid;
            user.AmountToBePaid += courseDTO.AmountToBePaid;
            if (!(await _appUserLogic.UpdateAsync(user))){ throw new Exception("Nie udalo sie zaaktualizowac wplaty"); };

            if(courseInstructor.CurrentlySigned + courseDTO.NumberOfPeopleSigningUp > courseInstructor.MaxPeople) { throw new Exception("Zbyt duza ilosc osob zapisuja sie na dany kurs"); }
            courseInstructor.CurrentlySigned += courseDTO.NumberOfPeopleSigningUp;
            if (!(await _courseAssigmentLogic.Update(courseInstructor))) { throw new Exception("Nie udalo sie zapisac na kurs"); };

            var courseUserAssigment = new CourseUserAssigment()
            {
                AppUser = user,
                AmountPaid = courseDTO.AmountPaid,
                AmountToBePaid = courseDTO.AmountToBePaid,
                NumberOfPeopleSigningUp = courseDTO.NumberOfPeopleSigningUp,
                CourseAssignment = courseInstructor,
                AppUserId = user.Id,
            };

            return await _courseUserAssigmentRepository.CreateAsync(courseUserAssigment);
        }

        public async Task<bool> Update(CourseUserAssigment course)
        {
            return await _courseUserAssigmentRepository.UpdateAsync(course);
        }

        public async Task<bool> Delete(int appUserId, int courseAssigmentId)
        {
            var courseInstructor = await _courseAssigmentLogic.GetById(courseAssigmentId);
            if (courseInstructor == null) { throw new Exception("Nie znaleziono kursu"); };

            var coursUserAssigment = await _courseUserAssigmentRepository.FindByIdAsync(appUserId, courseAssigmentId);
            if (coursUserAssigment == null) { throw new Exception("Nie znaleziono zapisanego uzytkownika do danego kursu"); }
            courseInstructor.CurrentlySigned -= coursUserAssigment.NumberOfPeopleSigningUp;

            if (!(await _courseAssigmentLogic.Update(courseInstructor))) { throw new Exception("Nie udalo sie zapisac kursu"); };
            return await _courseUserAssigmentRepository.DeleteAsync(appUserId, courseAssigmentId);
        }
    }
}
