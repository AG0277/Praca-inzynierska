using AutoMapper;
using Domain.Models;
using Infastructure.Utils;
using Logic.Dto.CourseAssigment;
using Logic.Logic.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace api.Controllers
{
    [ApiController]
    [Route("api/CourseAssigment")]
    public class CourseAssigmentController: ControllerBase
    {
        private readonly ICourseAssigmentLogic _courseAssigmentLogic;
        private readonly IMapper _mapper;

        public CourseAssigmentController(ICourseAssigmentLogic courseAssigmentLogic, IMapper mapper)
        {
            _courseAssigmentLogic = courseAssigmentLogic;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetCourseAssigment")]
        public async Task<IActionResult> GetCourseAssigment(int Id)
        {
            var reservatedRoom = await _courseAssigmentLogic.GetById(Id);
            if (reservatedRoom == null) return NotFound();
            return Ok(reservatedRoom);
        }

        [HttpGet]
        [Route("GetAllCourseAssigments")]
        public async Task<IActionResult> GetAllCourseAssigments()
        {
            var reservatedRoom = await _courseAssigmentLogic.GetAll();
            if (reservatedRoom == null || reservatedRoom.IsNullOrEmpty()) return NotFound();

            var result = reservatedRoom.Select(x => _mapper.Map<GetCourseAssigmentDto>(x));
            return Ok(result);
        }


        [HttpPost]
        [Authorize]
        [Route("CreateCourseAssigment")]
        public async Task<IActionResult> CreateCourseAssigment([FromBody] CreateCourseAssigmentDto courseAssigmentDto)
        {
            courseAssigmentDto.EndDate = Dates.NormalizeDate(courseAssigmentDto.EndDate);
            courseAssigmentDto.StartDate = Dates.NormalizeDate(courseAssigmentDto.StartDate);
            var mapped = _mapper.Map<CourseAssignment>(courseAssigmentDto);
            var res = await _courseAssigmentLogic.Create(mapped);
            return Ok(res);
        }

        [HttpPut]
        [Authorize]
        [Route("UpdateCourseAssigment")]
        public async Task<IActionResult> UpdateCourseAssigment([FromBody] UpdateCourseAssigmentDto courseAssigmentDto)
        {
            courseAssigmentDto.EndDate = Dates.NormalizeDate(courseAssigmentDto.EndDate);
            courseAssigmentDto.StartDate = Dates.NormalizeDate(courseAssigmentDto.StartDate);
            var mapped = _mapper.Map<CourseAssignment>(courseAssigmentDto);
            var res = await _courseAssigmentLogic.Update(mapped);
            return Ok(res);
        }

        [HttpDelete]
        [Authorize]
        [Route("DeleteCourseAssigment/{id}")]
        public async Task<IActionResult> DeleteCourseAssigment(int id)
        {
            var res = await _courseAssigmentLogic.Delete(id);
            if(res) return Ok(res);
            return NotFound();
        }
    }
}
