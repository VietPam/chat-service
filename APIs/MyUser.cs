using chat_service_se357.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;

namespace chat_service_se357.APIs
{
    public class MyUser
    {
        public MyUser() { }

        public async Task<bool> createUserAsync(string code, string name)
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
                context.users!.Add(sqlUser2);
                await context.SaveChangesAsync();
                return true;
            }
        }
    }
}
