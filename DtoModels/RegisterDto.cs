using System.ComponentModel.DataAnnotations;

namespace Full_proj.DtoModels;

public class RegisterDto
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string Email { get; set; }
    
}