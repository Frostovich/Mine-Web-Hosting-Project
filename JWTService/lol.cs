namespace Full_proj.JWTService;

using System.IdentityModel.Tokens.Jwt; 

using System.Security.Claims;

using Microsoft.IdentityModel.Tokens;

using System.Text;


public  class AuService
{
    public string GenerateToken(string username)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourVeryLongSecretKey123!"));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, "Admin")
        };

        var token = new JwtSecurityToken(
            issuer: "YourApp",
            audience: "YourAppUsers",
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
}
