using Domain.Abstractions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infastructure.Repositories
{
    public class AppUserRepository : BaseRepository<AppUser>, IAppUserRepository
    {
        public AppUserRepository(HostelDbContext hostelDbContext) : base(hostelDbContext) { }

        public async Task<AppUser?> GetByName(string FirstName, string LastName)
        {
            var result = await _dbSet.FirstOrDefaultAsync(n => n.FirstName == FirstName && n.LastName == LastName);
            if (result == null) return null;

            return result;
        }

        public async Task<AppUser?> GetByPhoneNumber(string PhoneNumber)
        {
            var result = await _dbSet.FirstOrDefaultAsync(n => n.PhoneNumber == PhoneNumber);
            if (result == null) return null;

            return result;
        }
    }
}
