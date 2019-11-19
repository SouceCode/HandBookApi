using System;
using System.ComponentModel.DataAnnotations;

namespace HandBookApi.Models
{
    /// <summary>
    /// 用户实体
    /// </summary>
    public class Users
    {
        [Display(Name = "用户ID")]
        public long Id { get; set; }
        [Display(Name = "用户名")]
        public string UserName { get; set; }
        [Display(Name = "密码")]
        public string PassWord { get; set; }
        [Display(Name = "创建时间")]
        public DateTime CreateDate { get; set; }

    }
}