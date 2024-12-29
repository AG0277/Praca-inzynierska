using Domain.Abstractions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infastructure.Repositories
{
    internal sealed class RoomRepository : BaseRepository<Room>, IRoomRepository
    {
        private readonly HostelDbContext _hostelDbContext;

        public RoomRepository(HostelDbContext hostelDbContext) : base(hostelDbContext)
        {
            _hostelDbContext = hostelDbContext;
        }

        //public async Task<List<Room>> GetByPhoneNumber(string phoneNumber)
        //{
        //     var listRoomReservations = await _hostelDbContext.RoomReservations.Where(x=>x.AppUser.PhoneNumber == phoneNumber).ToListAsync();
        //    var rooms = _hostelDbContext.Rooms.Where(x => x.RoomReservations);

        //}

        public async Task<List<RoomReservation>> BedsBetweenDate(int id, DateTime from, DateTime to)
        {
            return await _hostelDbContext.RoomReservations.Where(x => x.RoomId == id &&(
            (from >= x.From && from <= x.To) ||
            (to >= x.From && to <= x.To) ||
            (from <= x.From && to >= x.To)
            )).AsNoTracking().ToListAsync();
        }
        public async Task<List<RoomReservation>> AllBedsBetweenDate( DateTime from, DateTime to)
        {
            return await _hostelDbContext.RoomReservations.Where(x=>
            (from >= x.From && from <= x.To) ||
            (to >= x.From && to <= x.To) ||
            (from <= x.From && to >= x.To)
            ).AsNoTracking().ToListAsync();
        }
    }
}
