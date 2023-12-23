using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ImagePortal.API.Controllers
{
    [Route("Token")]
    [ApiController]
    public class TokenGenController : ControllerBase
    {

        [HttpGet("get-token")]
        public async Task<ActionResult> GenerateToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("79b6188c-cd95-4e75-b5db-7d2764bfe89b"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "lindsey"),
                new Claim(JwtRegisteredClaimNames.Email, "drewlindsey017@gmail.com"),
            };

            var token = new JwtSecurityToken(
                issuer: "Lindsey",
                audience: "Lucra",
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: credentials
            );

            var validToken = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(validToken.ToString());
        }
    }
}
