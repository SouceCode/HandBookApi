using System;
using System.ComponentModel.DataAnnotations;

namespace HandBookApi.Models
{
     /// <summary>
    /// 任务实体
    /// </summary>
    public class Job_Setting
    {
         
        [Display(Name = "任务ID")]
        public long Id { get; set; }
        [Display(Name = "任务名称")]
        public string Name{ get; set; }
           [Display(Name = "备注")]
        public string ReMark { get; set; }
         [Display(Name = "创建时间")]
        public DateTime CreateDate { get; set; }
          [Display(Name = "任务开关")]
         public bool IsClose { get; set; }
         [Display(Name = "用户Id")]
        public string UsersId{ get; set; }
        
    }
}