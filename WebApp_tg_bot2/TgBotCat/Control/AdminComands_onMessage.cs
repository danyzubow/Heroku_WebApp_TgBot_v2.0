using PorterOfChat.Bot;
using PorterOfChat.Bot.Model;
using PorterOfChat.Control.Admin_Cmd_OnCallBackQuery;
using PorterOfChat.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;

namespace PorterOfChat.Control.AdminComands_onMessage
{
    class adm : Command
    {
        public override string NameCommand { get; } = "/adm";
        protected override async void Execution(Message m)
        {

            new Chats().Exec(null, (CallbackQuery)null);
        }
    }
    class data : Command
    {
        public override string NameCommand { get; } = "/data";
        protected override async void Execution(Message m)
        {
            XmlSerializer Serial = new XmlSerializer(typeof(List<cChat>));
            string file = "Data.xml";

            List<cChat> chats = new List<cChat>();
            foreach (cChat t in Data.GetAllChats())
            {
                chats.Add((cChat)t.Clone());
            }

            if (System.IO.File.Exists(file))
            {
                System.IO.File.Delete(file);
            }
            using (FileStream fs = new FileStream(file, FileMode.CreateNew))
            {
                Serial.Serialize(fs, chats);
            }

            using (FileStream stream = new FileStream(file, FileMode.Open))
            {
                await _TgClient.SendDocumentAsync(Settings.AdminChatId, new InputOnlineFile(stream, file));
            }
            System.IO.File.Delete(file);
        }
    }
    class h : Command
    {
        public override string NameCommand { get; } = "/h";
        protected override async void Execution(Message m)
        {
            string outStr = "Command:\n/data";
            SendTextMessageAsync(Settings.AdminChatId, outStr);
        }
    }
    class ping : Command
    {
        public override string NameCommand { get; } = "/ping";
        protected override async void Execution(Message m)
        {

            new InfoService($"pong [{m.Date.ToString("G")} - > {DateTime.Now.ToString("G")}]");
        }
    }


}


