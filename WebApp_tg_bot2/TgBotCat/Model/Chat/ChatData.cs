using PorterOfChat.Bot;
using PorterOfChat.Bot.Model;
using PorterOfChat.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Xml.Serialization;
using Telegram.Bot.Types;


namespace PorterOfChat.Chat
{
    public class ChatData : IChat
    {
        public bool _Release { get; set; }
        public List<cChat> _Chats { get; private set; }
        bool SaveBool = false;
        Thread _saveAllThreaThread;

        public ChatData(bool Release)
        {

            _Chats = new List<cChat>();
            _Release = Release;
            DownloadSaves();
            LoadAllChats();
            _saveAllThreaThread = new Thread(Save_All_help);
            _saveAllThreaThread.Start();
        }
        #region Ftp

        public void DownloadSaves()
        {
            if (Settings.FtpContactDll == null) return;
            Assembly assembly = Assembly.LoadFile(Settings.FtpContactDll);//@"E:\ЛП\С#\Ftp-teste\Ftp-teste\ftp-contact"
            Type typeClass = assembly.GetType("ftp_contact");

            MethodInfo method = typeClass.GetMethod("Download");
            string path = Settings.DataFileXml_Name;
            method.Invoke(assembly, new object[] { path
            });
        }
        public void UploadSaves(string PATH = null)
        {
            Assembly assembly = Assembly.LoadFile(Settings.FtpContactDll);//@"E:\ЛП\С#\Ftp-teste\Ftp-teste\ftp-contact"
            Type typeClass = assembly.GetType("ftp_contact");

            MethodInfo method = typeClass.GetMethod("Upload");
            string path = (PATH == null ? Settings.DataFileXml_Name : PATH);
            method.Invoke(assembly, new object[] { path });
        }
        #endregion
        public void LoadAllChats()//
        {
            if (!System.IO.File.Exists(Settings.DataFileXml_Name)) return;
            XmlSerializer serial = new XmlSerializer(typeof(List<cChat>));

            #region File_to_db
            //  Console.WriteLine("---------LoadData delete code!!!");
            //  string text;
            //  using (StreamReader wr = new StreamReader(Settings.DataFileXml_Name))
            //  {
            //       text = wr.ReadToEnd();

            //  }

            //text=  text.Replace("<Id>", "<Id_tg>");
            //  text = text.Replace("</Id>", "</Id_tg>");
            //  using (StreamWriter wr=new StreamWriter(Settings.DataFileXml_Name,false))
            //  {
            //      wr.Write(text);

            //  }
            #endregion

            using (FileStream fs = new FileStream(Settings.DataFileXml_Name, FileMode.Open))
            {
                _Chats = (List<cChat>)serial.Deserialize(fs);
            }
            foreach (var group in _Chats)
            {

                group.LockGroupPidor = false;
                group.LockGroupDad = false;
                // group.Id_tg = group.Id.ToString();
                group.Id = 0;
            }

            //using (PorterDbContext chatdb = new PorterDbContext())
            //{

            //    foreach (var t in _Chats)
            //    {

            //        chatdb.chats.Add(t);
            //    }

            //    chatdb.SaveChanges();

            //}

        }

        public void SaveAll()//
        {
            SaveBool = true;
        }




        void Save_All_help() //
        {

            while (true)
            {
                if (SaveBool)
                {
                    SaveBool = false;
                    XmlSerializer Serial = new XmlSerializer(typeof(List<cChat>));

                    if (!Directory.Exists(Settings.Path))
                    {
                        Directory.CreateDirectory(Settings.Path);
                    }
                    System.IO.File.Delete(Settings.DataFileXml_Name);
                    using (FileStream fs = new FileStream(Settings.DataFileXml_Name, FileMode.CreateNew))
                    {
                        Serial.Serialize(fs, _Chats);
                    }
                    if (_Release)
                        UploadSaves();
                }
                Thread.Sleep(5000);

            }

        }

        public cChat GetChat(Message e)
        {
            return _Chats.FirstOrDefault(t => t.Id_tg == e.From.Id);
        }

        public cUser GetUser_FromMess(Message e)
        {
            return GetChat(e).users.FirstOrDefault(t => t.Id_tg == e.From.Id);
        }

        public cUser GetUser(int Id, Message e)
        {
            return GetChat(e).users.FirstOrDefault(t => t.Id_tg == Id);
        }

        public List<cChat> GetAllChats()
        {
            return _Chats;
        }

        public cChat GetChat(long Id)
        {
            return _Chats.FirstOrDefault(t => t.Id_tg == Id);
        }

        public void AddChat(cChat chat)
        {

            if (GetChat(chat.Id_tg) == null)
            {
                _Chats.Add(chat);
            }
            else
            {
                new InfoService($"Add chat: чат уже существует {chat.Id_tg}", InfoService.TypeMess.Error);
            }
        }

        public void Remove(cChat chat)
        {
            cChat chatL = GetChat(chat.Id_tg);
            if (chatL == null)
            {
                new InfoService($"Add chat: чат уже не существует {chat.Id_tg}", InfoService.TypeMess.Error);
            }
            else
            {
                _Chats.Remove(chatL);
            }

        }
    }

}