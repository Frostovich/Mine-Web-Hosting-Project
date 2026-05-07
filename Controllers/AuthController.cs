using Microsoft.AspNetCore.Mvc;
using Full_proj.Auth;
using Full_proj.DtoModels;


namespace Full_proj.Controllers;
[ApiController]
[Route("api/Rega")]
public class AuthController : ControllerBase
{
    private readonly AuthService _auService;
    public  AuthController(AuthService auService)
    {
        _auService = auService;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto model)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        var register = await _auService.Register(model.Username, model.Password, model.Email);
        return Ok(register);
    }
}