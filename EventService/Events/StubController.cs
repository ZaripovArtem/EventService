using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Features.Events
{
    /// <summary>
    /// Контроллер для проверки на аутентификацию
    /// </summary>
    [ApiController]
    [Route("[controller]/authstub")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class StubController : ControllerBase
    {
        /// <summary>
        /// Метод, требующий аутентификацию
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Task<IActionResult> Data()
        {
            return Task.FromResult<IActionResult>(Ok());
        }
    }
}
