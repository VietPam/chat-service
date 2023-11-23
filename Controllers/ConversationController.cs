using chat_service_se357.Models;
using Microsoft.AspNetCore.Mvc;

namespace chat_service_se357.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ConversationController: ControllerBase
    {
        [HttpGet]
        [Route("/getListConversation")]
        public async Task<IActionResult> getListConversationAsync(string code) {

            List<SqlConversation> conversations = await Program.api_conversation.getListConversationAsync(code);
            if(conversations.Count == 0)
            {
                return BadRequest();
            }
            else
            {
                return Ok(conversations);
            }
        }
    }
}
