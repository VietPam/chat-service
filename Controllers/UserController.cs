using Microsoft.AspNetCore.Mvc;

namespace chat_service_se357.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [Route("createUser")]
        public async Task<IActionResult> createUser(string code, string name, bool is_shop)
        {
            bool tmp = await Program.api_user.createUserAsync(code, name, is_shop);
            if (tmp)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("getListUser")]
        public async Task<IActionResult> getListUserAsync()
        {
            return Ok( await Program.api_user.getListUserAsync());
        }
    }
}
