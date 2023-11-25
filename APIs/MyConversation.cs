using chat_service_se357.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace chat_service_se357.APIs
{
    public class MyConversation
    {
        public MyConversation() { }

        public class MsgDTO
        {
            public string msg { get; set; }
            public string senderCode { get; set; }
            public string receiverCode { get; set; }
            public long time { get; set; } = 0;
        }
        public class ConversationDTOResponse
        {
            public long ConversationID { get; set; }
            public string their_name { get; set; }

            public MsgDTO msg { get; set; }
            public string avatar { get; set; }
            public long last_change { get; set; }
        }
        public async Task<bool> createConversation(string clientCode, string shopCode)
        {
            #region check null params
            if (string.IsNullOrEmpty(clientCode) || string.IsNullOrEmpty(shopCode))
            {
                return false;
            }
            #endregion


            #region Create new Conversation if not existing
            using (DataContext context = new DataContext())
            {
                #region lấy ra client và shop
                SqlUser? sqlClient = context.users!.Where(s => s.code == clientCode).FirstOrDefault();
                SqlUser? sqlShop = context.users!.Where(s => s.code == shopCode).FirstOrDefault();
                #endregion

                if (sqlShop == null || sqlShop == null) { return false; }
                SqlConversation? conversation = context.conversations!.Where(s => s.clientCode == clientCode && s.shopCode == shopCode).FirstOrDefault();
                if (conversation == null)
                {
                    SqlConversation tmp = new SqlConversation();
                    tmp.ID = DateTime.Now.Ticks;
                    tmp.clientCode = clientCode;
                    tmp.shopCode = shopCode;

                    // thêm conversation vào user
                    context.conversations!.Add(tmp);
                    await context.SaveChangesAsync();
                    Log.Information("Create new conversation");
                    return true;
                }
            }
            #endregion
            return false;
        }


        public async Task<List<ConversationDTOResponse>> getListConversationAsync(string code)
        {
            {
                if (string.IsNullOrEmpty(code))
                {
                    return null;
                }
                using (DataContext context = new DataContext())
                {
                    SqlUser? user = context.users!.Where(s => s.code == code).FirstOrDefault();
                    if (user == null)
                    {
                        return null;
                    }

                    List<SqlConversation>? list = new List<SqlConversation>();
                    //lấy ra list conversation
                    if (user.is_shop)
                    {
                        list = context.conversations.Where(s => s.shopCode == code).ToList();
                    }
                    else
                    {
                        list = context.conversations.Where(s => s.clientCode == code).ToList();
                    }
                    List<ConversationDTOResponse> response = new List<ConversationDTOResponse>();
                    foreach (SqlConversation conversation in list)
                    {
                        SqlMessage msg = context.messages!.Include(s => s.sqlConversation).Where(s => s.sqlConversation == conversation).ToList().Last();
                        MsgDTO msgDTO = new MsgDTO();

                        msgDTO.msg = msg.message;
                        msgDTO.senderCode = msg.senderCode;
                        msgDTO.receiverCode = msg.receiverCode;

                        //lấy ra thằng mà mình đang nhắn tin cùng
                        SqlUser? tmpUser = new SqlUser();
                        if (msg.senderCode == user.code)
                        {
                            tmpUser = context.users!.Where(s => s.code == msg.receiverCode).FirstOrDefault();
                        }
                        else
                        {
                            tmpUser = context.users!.Where(s => s.code == msg.senderCode).FirstOrDefault();
                        }

                        ConversationDTOResponse tmp = new ConversationDTOResponse();
                        tmp.ConversationID = conversation.ID;
                        tmp.their_name = tmpUser.name;
                        tmp.avatar = tmpUser.avatar;
                        tmp.msg = msgDTO;
                        tmp.last_change = conversation.last_change;
                        response.Add(tmp);
                    }
                    response.OrderBy(item => item.last_change);
                    return response;
                }
            }
        }

        public async Task<List<MsgDTO>> getListMsgInConvesation(long conversationID)
        {
            List<MsgDTO> nullResponse = new List<MsgDTO>();
            if (conversationID == null)
            {
                return nullResponse;
            }
            using (DataContext context = new DataContext())
            {
                List<SqlMessage>? listMsg = context.messages!.Include(s => s.sqlConversation).Where(s => s.sqlConversation.ID == conversationID).ToList();
                //SqlConversation? conversation = context.conversations!.Where(s => s.ID == conversationID).FirstOrDefault();
                List<MsgDTO> response = new List<MsgDTO>();
                foreach (SqlMessage message in listMsg)
                {
                    MsgDTO msg = new MsgDTO();
                    msg.time = message.time;
                    msg.msg = message.message;
                    msg.senderCode = message.senderCode;
                    msg.receiverCode = message.receiverCode;
                    response.Add(msg);
                }
                response.OrderBy(s => s.time);
                return response;
            }

            return nullResponse;
        }
    }
}
