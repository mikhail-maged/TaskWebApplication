using JWTAuthorization.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskWebApplication.Repository;

namespace TaskWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public AccountController(IUserRepository _userRepository)
        {
            userRepository = _userRepository;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> AccountLogin(UserLoginDto userLoginDto)
        {
            ValideToken token = await userRepository.UserLogin(userLoginDto);
            if (token.validation)
            {
                return Ok(token.UserToken);
            }

            return BadRequest(token.ErrorMessage);
        }


        [HttpPost("Register")]
        public async Task<IActionResult> AccountRegister(UserRegisterDto userRegisterDto)
        {

            var result = await userRepository.UserRegister(userRegisterDto);

            if (result.Succeeded)
            {
                return Ok();
            }



            return BadRequest(result.Errors);

        }

        [Authorize]
        [HttpGet("Test")]
        public IActionResult Accounttest()
        {
            return Ok(User?.Identity?.Name??"");
        }
    }
}
