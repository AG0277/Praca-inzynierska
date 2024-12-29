using api.Models;

namespace api.Repositories.Abstractions
{
    public interface IRoomReservationRepository:IRepository
    {

        public Task<RoomReservation?> GetByIds(int roomId, int appUserId);

        public Task<RoomReservation> Create(RoomReservation room);

        public Task<bool> Update(RoomReservation room);
    }
}
