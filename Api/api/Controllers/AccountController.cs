using Domain.Models;
using Logic.Dto.WorkerAccount;
using Logic.InternalServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/account")]
        [ApiController]
        public class AccountController : ControllerBase
        {
            private readonly UserManager<WorkerAccount> userManager;
            private readonly TokenService tokenService;
            private readonly SignInManager<WorkerAccount> signInManager;

            public AccountController(
                UserManager<WorkerAccount> UserManager,
                TokenService TokenService,
                SignInManager<WorkerAccount> SignInManager
            )
            {
                userManager = UserManager;
                tokenService = TokenService;
                signInManager = SignInManager;
            }

            [HttpPost("register")]
            [Authorize("Admin")]
            public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
            {
                try
                {
                    if (!ModelState.IsValid)
                        return BadRequest(ModelState);
                    var appUser = new WorkerAccount
                    {
                        UserName = registerDto.Username,
                        Email = registerDto.Email
                    };
                    var createdUser = await userManager.CreateAsync(appUser, registerDto.Password);
                    if (createdUser.Succeeded)
                    {
                        var roleResult = await userManager.AddToRoleAsync(appUser, "User");
                        if (roleResult.Succeeded)
                        {
                            return Ok(true);
                        }
                        else
                        {
                            return StatusCode(500, roleResult.Errors);
                        }
                    }
                    else
                    {
                        return StatusCode(500, createdUser.Errors);
                    }
                }
                catch (Exception e)
                {
                    return StatusCode(500, e);
                }
            }

            [HttpPost("login")]
            public async Task<IActionResult> Login([FromBody] LoginDto LoginDto)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var user = await userManager.Users.FirstOrDefaultAsync(x =>
                    x.UserName == LoginDto.Username.ToLower()
                );
                if (user == null)
                    return Unauthorized("Invalid credentials");
                var result = await signInManager.CheckPasswordSignInAsync(
                    user,
                    LoginDto.Password,
                    false
                );
                if (!result.Succeeded)
                    return Unauthorized("Invalid credentials");
                return Ok(
                    new UserWithToken
                    {
                        Username = user.UserName,
                        Email = user.Email,
                        Token = await tokenService.CreateToken(user)
                    }
                );
            }
        }
    
}
