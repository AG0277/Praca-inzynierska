using api.Models;

namespace api.Repositories.Abstractions
{
    public interface IAppUserRepository: IBaseRepository<AppUser>
    {
        public Task<AppUser?> GetByName(string FirstName, string LastName);
        public Task<AppUser?> GetByPhoneNumber(string PhoneNumber);
    }
}
