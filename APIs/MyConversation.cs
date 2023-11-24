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

                SqlConversation? conversation = context.conversations!.Where(s => s.clientCode == clientCode && s.shopCode == shopCode).Include(s => s.users).FirstOrDefault();
                if (conversation == null)
                {
                    SqlConversation tmp = new SqlConversation();
                    tmp.ID = DateTime.Now.Ticks;
                    tmp.clientCode = clientCode;
                    tmp.shopCode = shopCode;

                    // thêm conversation vào user
                    tmp.users.Add(sqlClient);
                    tmp.users.Add(sqlShop);
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
                    SqlUser? user = context.users!.Include(s => s.conversations).ThenInclude(cvstion => cvstion.messages).Where(s => s.code == code).FirstOrDefault();
                    if (user == null)
                    {
                        return null;
                    }
                    //pha xử lý cồng kềnh bởi vì mình chưa hiểu sql quan hệ nhiều - nhiều

                    if (user.is_shop)
                    {
                        List<SqlConversation> conversations = user.conversations!;
                        List<ConversationDTOResponse> response = new List<ConversationDTOResponse>();
                        foreach (SqlConversation conversation in conversations)
                        {
                            SqlMessage msg = context.messages!.Include(s => s.conversations).Where(s => s.conversations == conversation).ToList().Last();
                            MsgDTO msgDTO = new MsgDTO();

                            msgDTO.msg = msg.message;
                            msgDTO.senderCode = msg.senderCode;
                            msgDTO.receiverCode = msg.receiverCode;
                            if (msg.senderCode == user.code)
                            {
                                SqlUser? tmpUser = context.users!.Where(s => s.code == msg.receiverCode).FirstOrDefault();
                                ConversationDTOResponse tmp = new ConversationDTOResponse();
                                tmp.ConversationID = conversation.ID;
                                tmp.their_name = tmpUser.name;
                                tmp.avatar = tmpUser.avatar;
                                tmp.msg = msgDTO;
                                tmp.last_change = conversation.last_change;
                                response.Add(tmp);
                            }
                            else if (msg.receiverCode == user.code)
                            {
                                SqlUser? tmpUser = context.users!.Where(s => s.code == msg.senderCode).FirstOrDefault();
                                ConversationDTOResponse tmp = new ConversationDTOResponse();
                                tmp.ConversationID = conversation.ID;
                                tmp.their_name = tmpUser.name;
                                tmp.avatar = tmpUser.avatar;
                                tmp.msg = msgDTO;
                                tmp.last_change = conversation.last_change;
                                response.Add(tmp);
                            }
                        }
                        response.OrderBy(item => item.last_change);
                        return response;
                        //response.ConversationID 
                    }
                    else
                    {
                        //List<SqlConversation> conversations = context.conversations!.Where(s => s.clientCode== code).ToList();
                        //return conversations;
                    }
                }
                return new List<ConversationDTOResponse>();

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
                SqlConversation? conversation = context.conversations!.Include(s => s.messages).Where(s => s.ID == conversationID).FirstOrDefault();
                List<SqlMessage> messages = conversation.messages;
                List<MsgDTO> response = new List<MsgDTO>();
                foreach (SqlMessage message in messages)
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
