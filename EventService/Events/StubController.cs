using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Features.Events
{
    [ApiController]
    [Route("[controller]/authstub")]
    [Authorize]
    public class StubController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Data()
        {
            return Ok();
        }
    }
}
