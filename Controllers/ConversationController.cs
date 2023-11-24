using chat_service_se357.Models;
using Microsoft.AspNetCore.Mvc;
using static chat_service_se357.APIs.MyConversation;
using static chat_service_se357.APIs.MyConversation.MsgDTO;

namespace chat_service_se357.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ConversationController: ControllerBase
    {
        [HttpGet]
        [Route("/getListConversation")]
        public async Task<IActionResult> getListConversationAsync(string code) {

            List<ConversationDTOResponse> conversations = await Program.api_conversation.getListConversationAsync(code);
            if(conversations!=new List<ConversationDTOResponse>() )
            {
                return Ok(conversations);

            }
            else
            {
                return BadRequest();

            }
        }
    }
}
