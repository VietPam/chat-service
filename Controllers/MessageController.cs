using Microsoft.AspNetCore.Mvc;

namespace chat_service_se357.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        [HttpPost]
        [Route("createMessage")]
        public async Task<IActionResult> createMessageAsync(string senderCode, string receiverCode, string msg)
        {
            bool tmp = await Program.api_message.createMessageAsync(senderCode, receiverCode, msg);
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
