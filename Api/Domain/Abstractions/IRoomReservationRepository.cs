using Domain.Models;

namespace Domain.Abstractions
{
    public interface IRoomReservationRepository:IRepository
    {

        public Task<RoomReservation?> GetByIds(int roomId, int appUserId);
        public Task<List<RoomReservation>> GetReservationByUserId(int appUserId);

        public Task<RoomReservation> Create(RoomReservation room);

        public Task<bool> Update(RoomReservation room);
    }
}
