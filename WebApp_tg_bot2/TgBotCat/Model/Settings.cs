using System;
using System.Diagnostics;

namespace PorterOfChat.Bot
{


    internal static class Settings
    {
        public static int AdminChatId = 227950395;
        public static string NameBot; //  "@seadogs4_bot"
   

        public static string[] EmojPidor =
        {
            "👍", "👏", "🙏", "🤝", "👌", "💪", "✌️", "🏋️", "🐓", "🐿", "👨‍👨‍👦‍👦", "🚶", "🏃", "🏃‍", "👬",
            "👨‍👧", "👠", "🎯", "🚬", "⚰️"
        };


        /// <summary>
        /// Директория для Data. Н-п.: c:\data\ !!!
        /// </summary>
        public static string Path="";

        public static bool DebugMode=false;
        private static string DataFileXml_Name_;
        /// <summary>
        /// Файл xml хранение данных. обязательно Указать path!!
        /// </summary>
        public static string DataFileXml_Name
        {
            get
            {
                if(Path=="") throw new Exception("Не указан путь Path('Settings')");
                return Path + DataFileXml_Name_;
            }
            set { DataFileXml_Name_ = value; }
        }
         static string FtpContactDll_;

        public static string FtpContactDll
        {
            get
            {
                if (Path == "") throw new Exception("Не указан путь Path('Settings')");
                if (FtpContactDll_ == null) return null;
                return Path + FtpContactDll_;
            }
            set { FtpContactDll_ = value; }
        }


    }
}