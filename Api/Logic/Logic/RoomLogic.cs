using Domain.Abstractions;
using Domain.Models;
using Logic.Logic.Abstractions;
using Logic.Logic.Base;
using System.ComponentModel;

namespace Logic.Logic
{
    public sealed class RoomLogic : BaseLogic<Room>, IRoomLogic
    {
        private readonly IRoomRepository _roomRepository;

        public RoomLogic(IRoomRepository roomRepository) : base(roomRepository)
        {
            _roomRepository = roomRepository;
        }

        //public async Task<List<Room>> GetRoomsByPhoneNumber(string phoenNumber)
        //{
        //    return await _roomRepository.
        //}

        public async Task<List<Room>> GetAvailableBedsBetween(DateTime from, DateTime to)
        {
            var list = await _roomRepository.AllBedsBetweenDate(from, to);
            var roomsDic = (await _roomRepository.FindAllAsync()).ToDictionary(x => x.Id, x => x);
            if (roomsDic == null || roomsDic.Count <= 0) return new List<Room>();

            try
            {
                foreach (var element in list)
                {
                    if (roomsDic.ContainsKey(element.RoomId))
                    {
                        var room = roomsDic[element.RoomId];
                        room.NumberOfReservedBeds += element.NumberOfBeds;
                        roomsDic[element.RoomId] = room;
                    }
                }
                
            }
            catch(Exception e)
            {
                Console.WriteLine("XD");
            }
            var xd = roomsDic.Values.Where(x => x.NumberOfReservedBeds < x.NumberOfBeds).ToList();
            return xd;
        }


        public async Task IsAvaialbleBedAtDate(int roomid, DateTime from, DateTime to, int howManyPeople)
        {
            var list = await _roomRepository.BedsBetweenDate(roomid, from, to);
            var room = await _roomRepository.FindByIdAsync(roomid);
            if (room == null) return;

            var numberOfBedsOccupied = 0;
            foreach (var element in list)
            {
                numberOfBedsOccupied += element.NumberOfBeds;
            }

            if (room.NumberOfBeds - (numberOfBedsOccupied + howManyPeople) < 0)
                throw new Exception($"Pokój numer {room.RoomNumber} ma niewystraczającą ilość łóżek");
        }
    }
}
