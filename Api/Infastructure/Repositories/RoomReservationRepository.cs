using Domain.Abstractions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infastructure.Repositories
{
    internal sealed class RoomReservationRepository : IRoomReservationRepository
    {
        private readonly HostelDbContext _dbContext;
        private readonly DbSet<RoomReservation> _dbSet;

        public RoomReservationRepository(HostelDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<RoomReservation>();
        }

        public async Task<RoomReservation?> GetByIds(int roomId, int appUserId)
        {
            return await _dbSet.Include(x => x.Room).Include(x => x.AppUser).FirstOrDefaultAsync(x => x.RoomId == roomId && x.AppUserId == appUserId);
        }

        public async Task<List<RoomReservation>> GetReservationByUserId(int appUserId)
        {
            var xd = await _dbSet.Where(x => x.AppUserId == appUserId).ToListAsync();
            return xd;
        }
        public async Task<RoomReservation> Create(RoomReservation room)
        {
            await _dbSet.AddAsync(room);
            await _dbContext.SaveChangesAsync();
            return room;
        }

        public async Task<bool> Update(RoomReservation room)
        {
            var res = await _dbSet.FirstOrDefaultAsync(x => x.RoomId == room.RoomId && x.AppUserId == room.AppUserId);
            if (res == null) return false;

            _dbSet.Entry(res).CurrentValues.SetValues(room);
            await _dbContext.SaveChangesAsync();
            return true;
        }

    }
}
