using System;

namespace PorterOfChat.Bot.Model
{
    [Serializable]
    public class cUser:ICloneable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountPidor { get; set; }
        public int CountDad { get; set; }
        public int Id_tg { get; set; }
        public string FullName { get; set; }
        public bool GenderFemale { get; set; } = false;
        public override string ToString()
        {
            return $"{Name} [{FullName}]";
        }

        public object Clone()
        {
           return new cUser()
           {
               Id=this.Id,
               Name=this.Name,
               CountPidor = this.CountPidor,
               CountDad = this.CountDad,
               Id_tg = this.Id_tg,
               FullName = this.FullName,
               GenderFemale = this.GenderFemale
           };
        }

        public cChat cChat { get; set; }
    }

}