using AutoMapper;
using Domain.Models;
using Infastructure.Utils;
using Logic.Dto.RoomDtos;
using Logic.Logic.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/Room")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomLogic _roomLogic;
        private IMapper _mapper;

        public RoomController(IRoomLogic roomLogic, IMapper mapper)
        {
            _roomLogic = roomLogic;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllRooms")]
        public async Task<IActionResult> GetAllRooms()
        {
            var rooms = await _roomLogic.GetAllAsync();
            if (rooms.Count() <= 0) return NotFound();
            var roomsdto = rooms.Select(x => _mapper.Map<GetAllRoomsDto>(x));
            return Ok(roomsdto);
        }


        [HttpGet]
        [Route("GetRoomById/{id}")]
        public async Task<IActionResult> GetByRoomsId(int id)
        {
            var rooms = await _roomLogic.GetByIdAsync(id);
            if (rooms==null) return NotFound();
            var roomsdto = _mapper.Map<GetAllRoomsDto>(rooms);
            return Ok(roomsdto);
        }

        [HttpGet]
        [Route("GetAvailableRoomsAtDate")]
        public async Task<IActionResult> GetAvaiableRoomsAtDate([FromQuery] string from, string to)
        {
            DateTime.TryParse(from, out var parsedStartDate);
            DateTime.TryParse(to, out var parsedEndDate);
            var rooms = await _roomLogic.GetAvailableBedsBetween(Dates.NormalizeDate(parsedStartDate), Dates.NormalizeDate(parsedEndDate));
            if (rooms.Count() <= 0) return NotFound();
            return Ok(rooms);
        }

        [HttpPost]
        [Authorize]
        [Route("CreateRoom")]
        public async Task<IActionResult> CreateRoom(CreateRoomDto createRoomDto)
        {
            var room = _mapper.Map<Room>(createRoomDto);
            var createdRoom= await _roomLogic.AddAsync(room);
            return Ok(createdRoom);
        }

        [HttpPut]
        [Authorize]
        [Route("UpdateRoom")]
        public async Task<IActionResult> UpdateRoom(UpdateRoomDto updateRoomDto)
        {
            var room = _mapper.Map<Room>(updateRoomDto);
            var updatedRoom = await _roomLogic.UpdateAsync(room);
            if (updatedRoom == false) return NotFound();
            return Ok(updatedRoom);
        }

        [HttpDelete]
        [Authorize]
        [Route("DeleteRoom/{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var result = await _roomLogic.RemoveAsync(id);
            if (result == false) return NotFound();
            return Ok(result);
        }
    }
}
