using System.ComponentModel.DataAnnotations;

namespace Full_proj.DtoModels;

public class UserDto
{
    [Required]
    public string Username { get; set; }
}