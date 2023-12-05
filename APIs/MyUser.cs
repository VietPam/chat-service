using chat_service_se357.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Net.Sockets;

namespace chat_service_se357.APIs
{
    public class MyUser
    {
        public MyUser() { }

        public async Task<bool> createUserAsync(string code, string name, bool is_shop)
        {
            if( string.IsNullOrEmpty(code) || string.IsNullOrEmpty(name))
            {
                return false;
            }
            using ( DataContext context = new DataContext() ) 
            { 
                SqlUser? sqlUser = context.users!.Where(s=> s.code == code).FirstOrDefault();
                if ( sqlUser != null )
                {
                    return false;
                }

                SqlUser? sqlUser2 = new SqlUser();
                sqlUser2.ID = DateTime.Now.Ticks;
                sqlUser2.code= code;
                sqlUser2.name = name;
                sqlUser2.is_shop = is_shop;
                context.users!.Add(sqlUser2);
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<List<Response_User>> getListUserAsync()
        {
            List<Response_User> response = new List<Response_User>();
            using (DataContext context = new DataContext())
            {
                List<SqlUser> list = context.users!.ToList();
                foreach (SqlUser user in list)
                {
                    Response_User item = new Response_User();
                    item.code = user.code;
                    item.name = user.name;
                    item.is_shop = user.is_shop;
                    response.Add(item);
                }
            }
            return response;
        }


        public async Task<bool> disConnectUserAsync(string id)
        {
            using (DataContext context = new DataContext() )
            {
                try
                {
                    SqlUser? user = context.users.Where(s => s.IdHub.CompareTo(id) == 0).FirstOrDefault();
                    if (user == null)
                    {
                        return false;
                    }

                    user.IdHub = "";
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                    return false;
                }
            }
        }
        public async Task<bool> updateUserAsync(string idHub, string code)
        {
            using (DataContext context = new DataContext())
            {
                try
                {
                    SqlUser user = context.users.Where(s => s.code.CompareTo(code) == 0).FirstOrDefault();
                    if (user == null)
                    {
                        return false;
                    }
                    user.IdHub = idHub;
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                    return false;
                }
            }
        }
    }
}
