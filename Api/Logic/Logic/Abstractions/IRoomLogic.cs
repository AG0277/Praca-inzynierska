using api.Logic.Abstractions.Base;
using Domain.Abstractions;
using Domain.Models;

namespace Logic.Logic.Abstractions
{
    public interface IRoomLogic : IBaseLogic<Room>
    {
        public Task<List<Room>> GetAvailableBedsBetween(DateTime from, DateTime to);


        public Task IsAvaialbleBedAtDate(int roomid, DateTime from, DateTime to, int howManyPeople);
    }
}
