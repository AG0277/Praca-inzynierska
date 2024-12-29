using AutoMapper;
using Domain.Models;
using Logic.Dto.AppUser;
using Logic.Logic.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/AppUser")]
    public class AppUserController : ControllerBase
    {
        private readonly IAppUserLogic _appUserLogic;
        private IMapper _mapper;

        public AppUserController(IAppUserLogic AppUserLogic, IMapper mapper)
        {
            _appUserLogic = AppUserLogic;
            _mapper = mapper;
        }

        [HttpGet("GetUserByName")]
        public async Task<IActionResult> GetUserByName([FromQuery]string FirstName, string LastName)
        {
            var user = await _appUserLogic.GetByName(FirstName, LastName);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpGet]
        [Route("GetByPhoneNumber/{PhoneNumber}")]
        public async Task<IActionResult> GetUserByPhoneNumber(string PhoneNumber)
        {
            var user = await _appUserLogic.GetByPhoneNumber(PhoneNumber);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser(CreateAppUserDto createAppUserDto)
        {
            var appUser = _mapper.Map<AppUser>(createAppUserDto);
            var user = await _appUserLogic.AddAsync(appUser);
            return Ok(user);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UpdateAppUserDto updateAppUserDto)
        {
            var appUser = _mapper.Map<AppUser>(updateAppUserDto);
            var updatedUser = await _appUserLogic.UpdateAsync(appUser);
            if (updatedUser == false) return NotFound();
            return Ok(updatedUser);
        }
    }
}
