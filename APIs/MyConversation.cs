using chat_service_se357.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace chat_service_se357.APIs
{
    public class MyConversation
    {
        public MyConversation() { }

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

                SqlConversation? conversation = context.conversations!.Where(s => s.clientCode == clientCode && s.shopCode == shopCode).Include(s=>s.users).FirstOrDefault();
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


        public async Task<List<SqlConversation>> getListConversationAsync(string code)
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
                //pha xử lý cồng kềnh bởi vì mình chưa hiểu sql quan hệ nhiều - nhiều

                if (user.is_shop)
                {
                    List<SqlConversation> conversations = context.conversations!.Where(s => s.shopCode == code).ToList();
                    return conversations;
                }
                else
                {
                    List<SqlConversation> conversations = context.conversations!.Where(s => s.clientCode== code).ToList();
                    return conversations;
                }
            }

        }
    }
}
