using AutoMapper;
using Infastructure.Utils;
using Logic.Dto.RoomReservation;
using Logic.Dto.RoomReservationDto;
using Logic.Logic.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/RoomReservation")]
    public class RoomReservationController: ControllerBase
    {
        private readonly IRoomReservationLogic _roomReservationLogic;

        private readonly IMapper _mapper;

        public RoomReservationController(IRoomReservationLogic roomReservationLogic,IMapper mapper)
        {
            _roomReservationLogic = roomReservationLogic;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetRoomReservationByIds")]
        public async Task<IActionResult> GetReservatedRoom(int roomId, int appUserId)
        {
            var reservatedRoom = await _roomReservationLogic.GetByIds(roomId, appUserId);

            if (reservatedRoom == null) return NotFound();
            return Ok(_mapper.Map<GetRoomReservationDto>(reservatedRoom));
        }

        [HttpGet]
        [Route("GetRoomReservationByUserPhoneNumber/{phoneNumber}")]
        public async Task<IActionResult> GetRoomReservationByUserPhoneNumber(string phoneNumber)
        {
            var reservatedRoom = await _roomReservationLogic.GetByPhoneNumber(phoneNumber);

            if (reservatedRoom.Count <= 0) return NotFound();
            var xd= reservatedRoom.Select(x=>_mapper.Map<GetRoomReservationDto>(x));
            return Ok(xd);
        }


        [HttpPost]
        [Route("CreateRoomReservation")]
        public async Task<IActionResult> CreateRoomReservation([FromBody]CreateRoomReservationDto roomReservationDto)
        {
            roomReservationDto.From = Dates.NormalizeDate(roomReservationDto.From);
            roomReservationDto.To = Dates.NormalizeDate(roomReservationDto.To);
            var res = await _roomReservationLogic.Create(roomReservationDto);
            var result = await _roomReservationLogic.GetByIds(res.RoomId, res.AppUserId);

            if(result == null) return BadRequest();
            return Ok(_mapper.Map<GetRoomReservationDto>(result));
        }

    }
}
                                                