using AutoMapper;
using Domain.Models;
using Logic.Dto.Course;
using Logic.Logic.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/Course")]
    public class CourseController: ControllerBase
    {
        private readonly ICourseLogic _courseLogic;
        private readonly IMapper _mapper;

        public CourseController(ICourseLogic courseLogic, IMapper mapper)
        {
            _courseLogic = courseLogic;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllCourses")]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await _courseLogic.GetAllAsync();
            if (courses.Count() <= 0) return NotFound();
            return Ok(courses);
        }

        [HttpGet]
        [Route("GetCourseById/{id}")]
        public async Task<IActionResult> GetCourseById(int id)
        {
            var courses = await _courseLogic.GetByIdAsync(id);
            if (courses==null) return NotFound();
            return Ok(courses);
        }


        [HttpPost]
        [Authorize]
        [Route("CreateCourse")]
        public async Task<IActionResult> CreateCourse(CreateCourseDto createCourseDto)
        {
            var course = _mapper.Map<Course>(createCourseDto);
            var createdCourse = await _courseLogic.AddAsync(course);
            return Ok(createdCourse);
        }

        [HttpPut]
        [Authorize]
        [Route("UpdateCourse")]
        public async Task<IActionResult> UpdateCourse(UpdateCourseDto updateCourseDto)
        {
            var course = _mapper.Map<Course>(updateCourseDto);
            var updateCourse = await _courseLogic.UpdateAsync(course);
            if (updateCourse == false) return NotFound();
            return Ok(updateCourse);
        }

        [HttpDelete()]
        [Authorize]
        [Route("DeleteCourse/{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var result = await _courseLogic.RemoveAsync(id);
            if (result == false) return NotFound();
            return Ok(result);
        }
    }
}
