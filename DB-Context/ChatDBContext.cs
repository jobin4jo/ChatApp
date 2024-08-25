using ChatApp.Model.User;
using Microsoft.EntityFrameworkCore;
using System;

namespace ChatApp.DB_Context
{
    public class ChatDBContext: DbContext
    {
        public ChatDBContext(DbContextOptions<ChatDBContext> options): base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
    }
}
