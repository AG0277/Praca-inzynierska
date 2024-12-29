using Domain.Abstractions;
using Domain.Models;
using Logic.Logic.Abstractions;
using Logic.Logic.Base;

namespace Logic.Logic
{
    public sealed class AppUserLogic : BaseLogic<AppUser>, IAppUserLogic
    {
        private readonly IAppUserRepository _appUserRepository;

        public AppUserLogic(IAppUserRepository appUserRepository) : base(appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }
        public Task<AppUser?> GetByName(string FirstName, string LastName)
        {
            return _appUserRepository.GetByName(FirstName, LastName);

        }
        public Task<AppUser?> GetByPhoneNumber(string PhoneNumber)
        {
            return _appUserRepository.GetByPhoneNumber(PhoneNumber);
        }
    }
}
