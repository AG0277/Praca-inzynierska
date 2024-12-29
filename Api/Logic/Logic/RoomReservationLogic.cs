using AutoMapper;
using Domain.Abstractions;
using Domain.Models;
using Logic.Dto.RoomReservationDto;
using Logic.Logic.Abstractions;

namespace Logic.Logic
{
    public sealed class RoomReservationLogic: IRoomReservationLogic
    {
        private readonly IRoomReservationRepository _roomReservationRepository;
        private readonly IAppUserRepository _appUserRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;
        private readonly IRoomLogic _roomLogic;

        public RoomReservationLogic(IRoomReservationRepository roomReservationRepository, IAppUserRepository appUserRepository, IRoomRepository roomRepository, IMapper mapper,IRoomLogic roomLogic)
        {
            _roomReservationRepository = roomReservationRepository;
            _appUserRepository = appUserRepository;
            _roomRepository = roomRepository;
            _mapper = mapper;
            _roomLogic = roomLogic;
        }


        public async Task<RoomReservation?> GetByIds(int roomId, int appUserId)
        {
            return await _roomReservationRepository.GetByIds(roomId, appUserId);
        }

        public async Task<List<RoomReservation>> GetByPhoneNumber(string phoneNumber)
        {
            var user = await _appUserRepository.GetByPhoneNumber(phoneNumber);
            if (user == null) throw new Exception("User not found");
            return await _roomReservationRepository.GetReservationByUserId(user.Id);
        }

        public async Task<RoomReservation> Create(CreateRoomReservationDto roomdto)
        {
            var user = await _appUserRepository.FindByIdAsync(roomdto.AppUserId);
            var room = await _roomRepository.FindByIdAsync(roomdto.RoomId);
            if (user == null) throw new Exception("Nie znaleziono uzytkownika");
            if (room == null) throw new Exception("Nie znaleziono pokoju");

            await _roomLogic.IsAvaialbleBedAtDate(roomdto.RoomId, roomdto.From, roomdto.To,roomdto.NumberOfBeds);
            user.AmountToBePaid += roomdto.TotalPrice;

            var roomReservation = _mapper.Map<RoomReservation>(roomdto);
            roomReservation.TotalPrice = roomdto.TotalPrice;
            roomReservation.AppUser = user;
            roomReservation.Room = room;

            return await _roomReservationRepository.Create(roomReservation);
        }


    }
}
