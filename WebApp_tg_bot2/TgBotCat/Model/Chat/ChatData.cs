using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;
using PorterOfChat.Bot;
using PorterOfChat.Bot.Model;
using WebApp_tg_bot2.TgBotCat.Model.Chat;


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
            if (!File.Exists(Settings.DataFileXml_Name)) return;
            XmlSerializer serial = new XmlSerializer(typeof(List<cChat>));
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

            using (ChatDB chatdb = new ChatDB())
            {


                cChat ch1 = chatdb.chats.Include(q=>q.users).FirstOrDefault(t => t.Id_tg == "-182112682");
               

                var ch12 = chatdb.chats.Include(t=>t.users).ToList();
                ch1.Pidor = "You";
               // ch1.users[0].Name = "Xyu";
                foreach (var t in _Chats)
                {

                    chatdb.chats.Add(t);
                }

                chatdb.SaveChanges();

            }

        }

        public void Save_All()//
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
                    File.Delete(Settings.DataFileXml_Name);
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
    }

}