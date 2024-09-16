using ChatApp.Model.ChatMessage;
using ChatApp.Model.Payments;
using ChatApp.Model.User;
using Microsoft.EntityFrameworkCore;
using System;

namespace ChatApp.DB_Context
{
    public class ChatDBContext : DbContext
    {
        public ChatDBContext(DbContextOptions<ChatDBContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<ChatMessage> chatMessages { get; set; }
        public DbSet<Payments> payments { get; set; }
    }
}
