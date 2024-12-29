using AutoMapper;
using Domain.Models;
using Logic.Dto.CourseAssigment;
using Logic.Dto.CourseUserAssigment;
using Logic.Logic.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace api.Controllers
{
    [ApiController]
    [Route("api/CourseUserAssigment")]
    public class CourseUserAssigmentController: ControllerBase
    {

        private readonly ICourseUserAssigmentLogic _courseUserAssigmentLogic;

        public CourseUserAssigmentController(ICourseUserAssigmentLogic courseUserAssigmentLogic)
        {
             _courseUserAssigmentLogic =  courseUserAssigmentLogic;
        }

        [HttpGet]
        [Route("GetCourseUserAssigment")]
        public async Task<IActionResult> GetCourseUserAssigment(int appUserId, int courseAssigmentId)
        {
            var reservatedRoom = await _courseUserAssigmentLogic.GetByIds(appUserId, courseAssigmentId);
            if (reservatedRoom == null) return NotFound();
            return Ok(reservatedRoom);
        }


        [HttpPost]
        [Route("CreateCourseUserAssigment")]
        public async Task<IActionResult> CreateCourseUserAssigment([FromBody] CreateCourseUserAssigmentDto courseAssigmentDto)
        {
            var res = await _courseUserAssigmentLogic.Create(courseAssigmentDto);
            return Ok(res);
        }

        [HttpPut]
        [Route("UpdateCourseUserAssigment")]
        public async Task<IActionResult> UpdateCourseUserAssigment([FromBody] CourseUserAssigment courseUserAssigment)
        {
            var res = await _courseUserAssigmentLogic.Update(courseUserAssigment);
            return Ok(res);
        }
        [HttpDelete]
        [Route("DeleteCourseUserAssigment")]
        public async Task<IActionResult> DeleteCourseUserAssigment(int appUserId, int courseAssigmentId)
        {
            var res = await _courseUserAssigmentLogic.Delete( appUserId, courseAssigmentId);
            return Ok(res);
        }
    }
}
