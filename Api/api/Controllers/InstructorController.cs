using AutoMapper;
using Domain.Models;
using Logic.Dto.Instructor;
using Logic.Logic.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/Instructor")]
    public class InstructorController : ControllerBase
    {
        private IInstructorLogic _instructorLogic;
        private IMapper _mapper;

        public InstructorController(IInstructorLogic instructorLogic, IMapper mapper)
        {
            _instructorLogic = instructorLogic;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllInstructors")]
        public async Task<IActionResult> GetAllInstructors()
        {
            var instructors = await _instructorLogic.GetAllAsync();
            if (instructors.Count() <= 0) return NotFound();
            return Ok(instructors);
        }


        [HttpGet]
        [Route("GetInstructorById/{id}")]
        public async Task<IActionResult> GetInstructorById(int id)
        {
            var instructors = await _instructorLogic.GetByIdAsync(id);
            if (instructors == null) return NotFound();
            return Ok(instructors);
        }


        [HttpPost]
        [Authorize]
        [Route("CreateInstructor")]
        public async Task<IActionResult> CreateInstructor(CreateInstructorDto createInstructorDto)
        {
            var instructor = _mapper.Map<Instructor>(createInstructorDto);
            var createdRoom = await _instructorLogic.AddAsync(instructor);
            return Ok(createdRoom);
        }


        [HttpDelete]
        [Authorize]
        [Route("DeleteInstructor/{id}")]
        public async Task<IActionResult> DeleteInstructor(int id)
        {
            var result = await _instructorLogic.RemoveAsync(id);
            if (result == false) return NotFound();
            return Ok(result);
        }

        [HttpPut]
        [Authorize]
        [Route("UpdateInstructor")]
        public async Task<IActionResult> UpdateInstructor(Instructor instructor)
        {
            var result = await _instructorLogic.UpdateAsync(instructor);
            if (result == false) return NotFound();
            return Ok(result);
        }
    }
}
