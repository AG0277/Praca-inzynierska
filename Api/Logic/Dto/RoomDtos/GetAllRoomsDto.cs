using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Dto.RoomDtos
{
    public class GetAllRoomsDto
    {
        public int Id { get; set; }
        public int NumberOfBeds { get; set; }
        public int RoomNumber { get; set; }
        public int PricePerBed { get; set; }
        public string ImageBase64 { get; set; } = string.Empty;

    }
}
