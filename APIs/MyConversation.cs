using chat_service_se357.Models;
using Serilog;

namespace chat_service_se357.APIs
{
    public class MyConversation
    {
        public MyConversation() { }

        public async Task<SqlConversation> createConversation(string clientCode, string shopCode)
        {
            #region check null params
            if (string.IsNullOrEmpty(clientCode) || string.IsNullOrEmpty(shopCode))
            {
                return null;
            }
            #endregion

            #region Create new Conversation if not existing

            using (DataContext context = new DataContext())
            {
                SqlConversation? conversation = context.conversations!.Where(s => s.clientCode == clientCode && s.shopCode == shopCode).FirstOrDefault();
                if (conversation == null)
                {
                    SqlConversation tmp = new SqlConversation();
                    tmp.ID = DateTime.Now.Ticks;
                    tmp.clientCode = clientCode;
                    tmp.shopCode = shopCode;
                    context.conversations!.Add(tmp);
                    await context.SaveChangesAsync();
                    Log.Information("Create new conversation");
                    return tmp;
                }
            }
            #endregion
            return null;
        }
    }
}
