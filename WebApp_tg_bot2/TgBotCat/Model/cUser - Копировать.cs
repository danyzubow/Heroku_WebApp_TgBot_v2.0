using System;

namespace PorterOfChat.Bot.Model
{
    [Serializable]
    public class cUser_old
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CountPidor { get; set; }
        public string CountDad { get; set; }
        public string Id_tg { get; set; }
        public string FullName { get; set; }
        public bool GenderFemale { get; set; } = false;

        public cChat cChat_old { get; set; }
    }

}