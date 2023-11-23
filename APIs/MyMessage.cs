using chat_service_se357.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Net.Mime;

namespace chat_service_se357.APIs
{
    public class MyMessage
    {
        public MyMessage() { }
        public async Task<bool> createMessageAsync(string senderCode, string receiverCode, string msg)
        {
            #region Check null
            if (string.IsNullOrEmpty(senderCode) || string.IsNullOrEmpty(receiverCode) || string.IsNullOrEmpty(msg))
            {
                return false;
            }
            #endregion
            using (DataContext context = new DataContext())
            {


                #region indentify who is shop, who is client 
                // xem thử thằng gửi là thằng shop hay thằng client
                SqlUser? shop = context.users!.Where(s => s.is_shop && s.code == senderCode).FirstOrDefault();
                SqlUser? client = new SqlUser();
                if (shop == null)
                {
                    shop = context.users!.Where(s => s.is_shop && s.code == receiverCode).FirstOrDefault();
                    client = context.users!.Where(s => s.is_shop == false && s.code == senderCode).FirstOrDefault();
                }
                else
                {
                    client = context.users!.Where(s => s.is_shop == false && s.code == receiverCode).FirstOrDefault();
                }
                #region check if both is client/shop
                if (client == null || shop == null)
                {
                    return false;
                }

                #endregion


                Log.Information("ID shop: {0}, name: {1}", shop.ID, shop.name);
                Log.Information("ID client: {0}, name:{1}", client.ID, client.name);
                #endregion

                #region check existed Conversation ? then create new Conversation
                // tìm xem có conversation của sender và receiver chưa, nếu chưa thì tạo mới conversation
                SqlConversation? sqlConversation = context.conversations!.Where(s => s.clientCode == client.code && s.shopCode == shop.code).Include(s => s.messages).FirstOrDefault();
                if (sqlConversation == null)
                {
                    sqlConversation = await Program.api_conversation.createConversation(client.code, shop.code);
                }
                #endregion

                #region Create new msg
                //ròi tạo mới message gán vào conversation đó
                SqlMessage message = new SqlMessage();
                message.ID = DateTime.Now.Ticks;
                message.clientCode = client.code;
                message.shopCode = shop.code;
                message.message = msg;
                context.messages.Add(message);
                sqlConversation.messages.Add(message);
                await context.SaveChangesAsync();

                return true;

                #endregion

            }
        }
    }
}
