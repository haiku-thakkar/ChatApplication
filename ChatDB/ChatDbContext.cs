﻿
using SlackClone.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.SignalR.Client;

namespace SlackClone.ChatDB
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options)
           : base(options) { }

        public DbSet<Login> Login { get; set; }
        public DbSet<MessageModel> MessageModel { get; set; }
    }
}
