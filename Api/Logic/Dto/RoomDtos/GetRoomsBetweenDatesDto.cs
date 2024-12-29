using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Dto.RoomDtos
{
    public class GetRoomsBetweenDatesDto
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
