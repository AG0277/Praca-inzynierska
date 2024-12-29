using api.Logic.Abstractions.Base;
using Domain.Models;

namespace Logic.Logic.Abstractions
{
    public interface IAppUserLogic : IBaseLogic<AppUser>
    {
        public Task<AppUser?> GetByName(string FirstName, string LastName);
        public Task<AppUser?> GetByPhoneNumber(string PhoneNumber);
    }
}
