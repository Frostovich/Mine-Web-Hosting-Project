namespace Full_proj.Controllers;
using Microsoft.AspNetCore.Mvc;
using Full_proj.DtoModels;
using Full_proj.repository;
[ApiController]
[Route("api/search")]
public class SearchUserController : ControllerBase
{
    private readonly ILogger<SearchUserController> _logger;
    private readonly IUserRepository _userRepository;

    public SearchUserController(ILogger<SearchUserController> logger, IUserRepository userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }
    [HttpGet("SearchUser")]
    public IActionResult SearchUser(UserDto model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
           _userRepository.GetUserByUsername(model.Username);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogInformation("You enter no parameter, or something went wrong");
            return BadRequest(ex.Message);
        }
    }
    
}
