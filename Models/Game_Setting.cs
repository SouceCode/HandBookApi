using System;
using System.ComponentModel.DataAnnotations;

namespace HandBookApi.Models
{
     /// <summary>
    /// 游戏试玩实体
    /// </summary>
    public class Game_Setting
    {
         
        [Display(Name = "游戏试玩ID")]
        public long Id { get; set; }
        [Display(Name = "游戏试玩Url")]
        public string Url{ get; set; }
        [Display(Name = "用户名")]
        public string UserName { get; set; }
        [Display(Name = "密码")]
         public string PassWord { get; set; }
           [Display(Name = "备注")]
        public string ReMark { get; set; }
         [Display(Name = "创建时间")]
        public DateTime CreateDate { get; set; }
         [Display(Name = "截止时间")]
        public DateTime DeadLine { get; set; }
         [Display(Name = "试玩类型")]
        public TryTypeEnum TryType { get; set; }
        
    }
}