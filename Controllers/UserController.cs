using Microsoft.AspNetCore.Mvc;

namespace chat_service_se357.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [Route("createUser")]
        public async Task<IActionResult> createUser(string code, string name)
        {
            bool tmp = await Program.api_user.createUserAsync(code, name);
            if (tmp)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
