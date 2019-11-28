using HandBookApi.Models;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HandBookApi.Job
{
    public class EmailJob : BackgroundService
    {
         private readonly HandBookContext _context;
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                await new TaskFactory().StartNew(() =>
                {
                    try
                    {


                        //定时任务业务逻辑,比如:
                        string value = DateTime.Now.ToString();
                        StreamWriter sw = new StreamWriter(@"D:\1.txt", true);//true有新数据继续写,false后边的数据覆盖前边的
                        sw.WriteLine("执行时间： " + value;
                        sw.Flush();
                        sw.Close();


                        //满足某种条件执行 比如每天凌晨执行
                        var time = DateTime.Now.ToString("HH:mm:ss");
                        if ("00:01:00" == time)
                        {
                            //业务逻辑 
                            Console.WriteLine(DateTime.Now + ":进入这里了");

                        }

                    }
                    catch (Exception exp)
                    {
                        //错误处理
                    }

                    //定时任务休眠
                    Thread.Sleep(1 * 1000);
                });
            }



        }
    }
}