using Domain.Models;

namespace Domain.Abstractions
{
    public interface IRoomRepository : IBaseRepository<Room>
    {
        public Task<List<RoomReservation>> BedsBetweenDate(int id, DateTime from, DateTime to);
        public Task<List<RoomReservation>> AllBedsBetweenDate(DateTime from, DateTime to);
    }
}
