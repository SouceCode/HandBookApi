using HandBookApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration.Json;

namespace HandBookApi.Job
{
    public class EmailJob : BackgroundService
    {

 public static IConfiguration Configuration { get; set; }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //读取appsettings.json配置信息
            //ReloadOnChange = true 当appsettings.json被修改时重新加载            
            Configuration = new ConfigurationBuilder()
            　　.Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true })
            　　.Build();            
           //创建NetCore命令行日志
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddFilter("HandBookApi.Job.EmailJob", LogLevel.Debug)
                    .AddConsole()
                    .AddEventLog();
            });
            ILogger logger = loggerFactory.CreateLogger<EmailJob>();


            while (!stoppingToken.IsCancellationRequested)
            {
                await new TaskFactory().StartNew(() =>
                {
                    try
                    {


                        var builder = new DbContextOptionsBuilder<HandBookSqlServerContext>();


                        builder.UseSqlServer(Configuration.GetConnectionString("HandBookSqlServerContext"));
                        using (HandBookSqlServerContext _context = new HandBookSqlServerContext(builder.Options))
                        {

                            //是否有定时任务
                            List<Job_Setting> job_Setting = new List<Job_Setting>();
                            if (_context.Database.IsSqlServer())
                            {
                                /***
                                          *由于EF6.0不支持SQLSERVER 2008 R2
                                          *现分页采用原生的ADO.NET方式来实现
                                          *等效于
                                          *
                                          ***/
                                string table = "Job_Settings";
                                int pageSize = 10;//页面记录数
                                int pageNumber = 0;//页码
                                string where = "  Where IsClose=0 ";
                                string orderby = " order by id asc";

                                string sqlstr = @"select * from (select row_number() over ( " + orderby + " ) as rownum,* from " + table + @"  With(NOLOCK)   " + where + ")A where rownum >" + pageSize * (pageNumber) + " and rownum <= " + pageSize * (pageNumber + 1);
                                job_Setting = _context.Job_Settings.FromSqlRaw(sqlstr).AsNoTracking()

                                .ToList<Job_Setting>();

                            }
                            else if (_context.Database.IsSqlite())
                            {
                                string table = "Job_Settings";
                                int pageSize = 10;//页面记录数
                                int pageNumber = 1;//页码
                                string where = " Where IsClose=0 ";
                                string orderby = " order by id asc";

                                string sqlstr = "select * from " + table + " " + where + orderby;
                                job_Setting = _context.Job_Settings.FromSqlRaw(sqlstr).AsNoTracking()
                                 .Skip(pageSize * pageNumber)
                                 .Take(pageSize)
                                 .ToList<Job_Setting>();

                            }
                            else
                            {
                                //do something

                            }

                            if (job_Setting.Count > 0)
                            {//是否开启定时任务

                                //定时任务业务逻辑,比如:
                                // string value = DateTime.Now.ToString();
                                // StreamWriter sw = new StreamWriter(@"C:\HandBookApi\log\1.txt", true);//true有新数据继续写,false后边的数据覆盖前边的
                                // sw.WriteLine("执行时间： " + value);
                                // sw.Flush();
                                // sw.Close();


                                //满足某种条件执行 比如每天凌晨执行
                                var time = DateTime.Now.ToString("HH:mm:ss");
                                if ("00:01:00" == time)
                                {
                                    //业务逻辑 
                                    string table = " Game_Settings gs";
                                    string where = " where 1=1 And devices=1  And IsCompleted=0 And DeadLine>'"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"'";
                                    //devices：0电脑1手机
                                    //IsCompleted=0 是否领奖：0否1是
                                    string orderby = "    order by deadLine asc ";
                                    int pageindex = 0;
                                    int size = 20;

                                    List<Game_Setting> game_Settings = new List<Game_Setting>();
                                    if (_context.Database.IsSqlServer())
                                    {
                                        /***
                                                  *由于EF6.0不支持SQLSERVER 2008 R2
                                                  *现分页采用原生的ADO.NET方式来实现
                                                  *等效于
                                                  *
                                                  ***/
                                        int pageSize = size;//页面记录数
                                        int pageNumber = pageindex;//页码

                                        if (string.IsNullOrEmpty(orderby))
                                        {
                                            orderby = " order by id asc";
                                        }
                                        string sqlstr = @"select * from (select row_number() over ( " + orderby + " ) as rownum,* from " + table + @"  With(NOLOCK)   " + where + ")A where rownum >" + pageSize * (pageNumber) + " and rownum <= " + pageSize * (pageNumber + 1);
                                        game_Settings = _context.Game_Settings.FromSqlRaw(sqlstr).AsNoTracking()

                                         .ToList<Game_Setting>();


                                    }
                                    else if (_context.Database.IsSqlite())
                                    {

                                        int pageSize = size;//页面记录数
                                        int pageNumber = pageindex;//页码
                                        if (string.IsNullOrEmpty(orderby))
                                        {
                                            orderby = " order by id asc";
                                        }

                                        string sqlstr = "select * from " + table + " " + where + orderby;
                                        game_Settings = _context.Game_Settings.FromSqlRaw(sqlstr).AsNoTracking()
                                        .Skip(pageSize * pageNumber)
                                        .Take(pageSize)
                                        .ToList<Game_Setting>();


                                    }
                                    else
                                    {


                                    }

                                    //拼凑信息提示内容
                                    string s = string.Empty;
                                    for (var i = 0; i < game_Settings.Count; i++)
                                    {
                                        s += "<br/>";
                                        s += " 试玩地址：" + game_Settings[i].Url + "<br/>";                                   
                                        s += " &nbsp;&nbsp;&nbsp;&nbsp;账号：" + game_Settings[i].UserName + "<br/>";
                                        s += " &nbsp;&nbsp;&nbsp;&nbsp;密码：" + game_Settings[i].PassWord + "<br/>";
                                        s += " &nbsp;&nbsp;&nbsp;&nbsp;试玩类型：" + GetTryTypeEnum(Convert.ToInt32(game_Settings[i].TryType)) + "<br/>";
                                        s += " &nbsp;&nbsp;&nbsp;&nbsp;截至日期：" + game_Settings[i].DeadLine + "<br/>";
                                        s += " &nbsp;&nbsp;&nbsp;&nbsp;备注：" + game_Settings[i].ReMark + "<br/>";
                                       
                                    }




                                    var message = new MimeMessage();
                                    //接受方
                                    message.To.Add(new MailboxAddress(Configuration["To:Nick"], Configuration["To:Address"]));
                                    //发送方
                                    message.From.Add(new MailboxAddress(Configuration["From:Nick"], Configuration["From:Address"]));
                                    
                                    //标题
                                    message.Subject = DateTime.Now+"目前有"+game_Settings.Count+"个待试玩任务,注意查收";
                                    //文字内容
                                    // message.Body = new TextPart("plain") { Text = "上海一日游"  };
                                    message.Body = new TextPart("Html") { Text = s  };
                                   
                                    //开始发送
                                    using (var client = new MailKit.Net.Smtp.SmtpClient())
                                    {

                                        client.Connect("smtp.163.com", 25, false);

                                        client.Authenticate(Configuration["From:Address"], Configuration["From:Password"]);
                                        client.Send(message);
                                        client.Disconnect(true);
                                    }
                                    logger.LogInformation("成功发送邮件提醒");


                                    // Console.WriteLine(DateTime.Now + ":进入这里了");

                                }
                            }
                        }







                    }
                    catch (Exception exp)
                    {
                        logger.LogError(exp.Message+exp.StackTrace);
                        //错误处理
                    }

                    //定时任务休眠
                    Thread.Sleep(1 * 1000);
                });
            }



        }

        private String GetTryTypeEnum(int str)
        {

            var retrunstr = "";
            switch (str)
            {
                case (int)TryTypeEnum.练级:
                    retrunstr = "练级";
                    break;

                case (int)TryTypeEnum.时长:
                    retrunstr = "时长";
                    break;
                case (int)TryTypeEnum.练战斗力:
                    retrunstr = "练战斗力";
                    break;

            }
            return retrunstr;
        }

    }
}