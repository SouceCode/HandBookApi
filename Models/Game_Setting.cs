using System;

namespace HandBookApi.Models
{
    public class Game_Setting
    {
        public long Id { get; set; }
        public string Url{ get; set; }
        public string UserName { get; set; }
         public string PassWord { get; set; }
        public string ReMark { get; set; }
        public DateTime CreateDate { get; set; }
    }
}