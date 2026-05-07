using Full_proj.DtoModels;

namespace Full_proj.Controllers;
using Microsoft.AspNetCore.Mvc;
using Full_proj.Auth;
[ApiController]
[Route("api/login")]
public class LoginController : ControllerBase
{
    private readonly AuthService _authService;

    public LoginController(AuthService authService)
    {
        _authService = authService;
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]LoginDto model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var loginService = await _authService.Login(model.Username, model.Password);
        if (string.IsNullOrEmpty(loginService))
            return Unauthorized(new { message = "Invalid username or password" });
        return Ok(loginService);
    }
}


