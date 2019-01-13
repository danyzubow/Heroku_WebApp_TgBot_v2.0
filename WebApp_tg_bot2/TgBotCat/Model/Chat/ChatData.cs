

using cat.Bot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Xml.Serialization;
using cat.Bot.Model;


namespace cat.Chat
{
    public class ChatData : IChat
    {
        public bool _Release { get; set; }
        public List<cChat> _Chats { get; private set; }
        bool SaveBool = false;
        Thread _saveAllThreaThread;

        public ChatData( bool Release)
        {
          
            _Chats=new List<cChat>();
            _Release = Release;
            DownloadSaves();
            LoadAllChats();
            _saveAllThreaThread = new Thread(Save_All_help);
            _saveAllThreaThread.Start();
        }
        #region Ftp

        public void DownloadSaves()
        {
            if (Information.FtpContactDll == null) return;
            Assembly assembly = Assembly.LoadFile(Information.FtpContactDll);//@"E:\ЛП\С#\Ftp-teste\Ftp-teste\ftp-contact"
            Type typeClass = assembly.GetType("ftp_contact");

            MethodInfo method = typeClass.GetMethod("Download");
            string path = Information.DataFileXml_Name;
            method.Invoke(assembly, new object[] { path
            });
        }
        public void UploadSaves(string PATH = null)
        {
            Assembly assembly = Assembly.LoadFile(Information.FtpContactDll);//@"E:\ЛП\С#\Ftp-teste\Ftp-teste\ftp-contact"
            Type typeClass = assembly.GetType("ftp_contact");

            MethodInfo method = typeClass.GetMethod("Upload");
            string path = (PATH == null ? Information.DataFileXml_Name : PATH);
            method.Invoke(assembly, new object[] { path });
        }
        #endregion
        public void LoadAllChats()//
        {
            if (!File.Exists(Information.DataFileXml_Name)) return;
            XmlSerializer serial = new XmlSerializer(typeof(List<cChat>));
            using (FileStream fs = new FileStream(Information.DataFileXml_Name, FileMode.Open))
            {
                _Chats = (List<cChat>)serial.Deserialize(fs);
            }
            foreach (var group in _Chats)
            {
                group.LockGroupPidor = false;
                group.LockGroupDad = false;

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
                   
                    if (!Directory.Exists(Information.Path))
                    {
                        Directory.CreateDirectory(Information.Path);
                    }
                    File.Delete(Information.DataFileXml_Name);
                    using (FileStream fs = new FileStream(Information.DataFileXml_Name, FileMode.CreateNew))
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