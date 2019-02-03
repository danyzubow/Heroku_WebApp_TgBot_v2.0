﻿using Microsoft.EntityFrameworkCore;
using PorterOfChat.Bot.Model;
using PorterOfChat.Chat;
using PorterOfChat.Service;
using System.Collections.Generic;
using System.Linq;
using PorterOfChat.Bot;
using Telegram.Bot.Types;

namespace WebApp_tg_bot2.TgBotCat.Model.Chat
{
    public class PorterDbContext : DbContext, IChat
    {

        public DbSet<cChat> chats { get; set; }
        public DbSet<cUser> users { get; set; }
        public DbSet<MessageLog> MessagesLogs { get; set; }
        private bool Debug;
        public PorterDbContext(DbContextOptions<PorterDbContext> options)
            : base(options)
        {
        }
        public PorterDbContext(bool Debug)
        {
            Database.EnsureCreated();
            this.Debug = Debug;
            chats.Include(t => t.users).Load();
        }
        public PorterDbContext()
        {
            Database.EnsureCreated();
            //this.Debug = Debug;
            //chats.Include(t => t.users).Load();
        }
        public cChat GetChat(Message e)
        {
            return GetChat(e.Chat.Id);
        }
        public cUser GetUser_FromMess(Message e)
        {
            cChat chat = GetChat(e);
            return chat.users.FirstOrDefault(t => t.Id_tg == e.From.Id);
        }
        public cUser GetUser(int Id, Message e)
        {
            cChat chat = GetChat(e);
            return chat.users.FirstOrDefault(t => t.Id_tg == Id);
        }

        public List<cChat> GetAllChats()
        {
            return chats.Local.ToList();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Settings.ConnectionString);
        }

        public void SaveAll()
        {
            if (Debug) return;
            SaveChangesAsync();
        }

        public void AddChat(cChat chat)
        {
            if (GetChat(chat.Id_tg) == null)
            {
                chats.Add(chat);
            }
            else
            {
                new InfoService($"Add chat: чат уже существует {chat.Id_tg}", InfoService.TypeMess.Error);
            }
        }

        public  void Remove(cChat chat)
        {
            cChat chatLocal = GetChat(chat.Id_tg);
            if (chatLocal == null)
            {
                new InfoService($"Add chat: чат уже не существует {chat.Id_tg}", InfoService.TypeMess.Error);
            }
            else
            {
                chats.Remove(chatLocal);
            }
        }

        public cChat GetChat(long Id)
        {
            cChat cChat = chats.Local.FirstOrDefault(t => t.Id_tg == Id);
            if (cChat == null)
            {
                cChat = chats.Local.FirstOrDefault(t => t.Id_tg == Id);
            }
            return cChat;
        }

        public void AddMessage(MessageLog msg)
        {
            MessagesLogs.Add(msg);
        }

        public List<MessageLog> GetMessageLogs()
        {
            MessagesLogs.Include(t=>t.chat).Include(t=>t.user).Load();
            return MessagesLogs.ToList();
        }
    }
}
