using Features.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Features.Events
{
    /// <summary>
    /// Контроллер для проверки на аутентификацию
    /// </summary>
    [ApiController]
    [Route("[controller]/authstub")]
    
    public class StubController : ControllerBase
    {
        /// <summary>
        /// Метод для получения JWT token'а
        /// </summary>
        /// <param name="nickname">Nickname пользователя</param>
        /// <returns></returns>
        [HttpGet("/Token/{nickname}")]
        public string GetJwtToken(string nickname)
        {
            var claims = new List<Claim> { new (ClaimTypes.Name, nickname) };
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.Issuer,
                audience: AuthOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        /// <summary>
        /// Метод, требующий аутентификацию
        /// </summary>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public Task<IActionResult> Data()
        {
            return Task.FromResult<IActionResult>(Ok());
        }
    }
}
