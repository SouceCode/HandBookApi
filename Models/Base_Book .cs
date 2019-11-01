using System;
using System.ComponentModel.DataAnnotations;

namespace HandBookApi.Models
{ /// <summary>
    /// 号码簿实体
    /// </summary>
    public class Base_Book
    {
          
        [Display(Name = "号码簿ID")]
        public long Id { get; set; }
         [Display(Name = "号码簿名称")]
        public string Name { get; set; }
          [Display(Name = "备注")]
        public string ReMark { get; set; }
            [Display(Name = "创建时间")]
        public DateTime CreateDate { get; set; }
    }
}