using DataAccessLayers;
using Microsoft.AspNetCore.Mvc;
using WebApplication1_API.Repository;

namespace WebApplication1_API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
       private IUserInterface _userRepository;
        public AuthenticationController(IUserInterface userRepository)
        {
            _userRepository = userRepository;   
        }
  
        // /api/auth/register
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userRepository.RegisterUser(model);

                if (result.IsSuccess)
                    return Ok(result); // Status Code: 200 

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid"); // Status code: 400
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] loginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userRepository.Login(model);

                if (result.IsSuccess)
                    return Ok(result); // Status Code: 200 

                return Ok(result);
            }

            return Ok("Some properties are not valid"); // Status code: 400
        }
    }
}
