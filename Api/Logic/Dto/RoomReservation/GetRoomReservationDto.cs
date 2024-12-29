using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Dto.RoomReservation
{
    public class GetRoomReservationDto
    {
        public int ReservationId { get; set; }
        public int AppUserId { get; set; }
        public int RoomId { get; set; }
        public int NumberOfBeds { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int AmountPaid { get; set; }
        public int TotalPrice { get; set; }
    }
}
