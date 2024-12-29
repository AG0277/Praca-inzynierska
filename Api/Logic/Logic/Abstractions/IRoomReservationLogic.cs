using Domain.Models;
using Logic.Dto.RoomReservationDto;

namespace Logic.Logic.Abstractions
{
    public interface IRoomReservationLogic:ILogic
    {
        public Task<RoomReservation?> GetByIds(int roomId, int appUserId);

        public Task<RoomReservation> Create(CreateRoomReservationDto roomdto);
        public  Task<List<RoomReservation>> GetByPhoneNumber(string phoneNumber);
    }
}
