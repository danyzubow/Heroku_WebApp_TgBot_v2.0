using System;

namespace cat.Bot
{


    internal static class Information
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

        private static string DataFileXml_Name_;
        /// <summary>
        /// Файл xml хранение данных. обязательно Указать path!!
        /// </summary>
        public static string DataFileXml_Name
        {
            get
            {
                if(Path=="") throw new Exception("Не указан путь Path('Information')");
                return Path + DataFileXml_Name_;
            }
            set { DataFileXml_Name_ = value; }
        }
         static string FtpContactDll_;

        public static string FtpContactDll
        {
            get
            {
                if (Path == "") throw new Exception("Не указан путь Path('Information')");
                if (FtpContactDll_ == null) return null;
                return Path + FtpContactDll_;
            }
            set { FtpContactDll_ = value; }
        }


    }
}