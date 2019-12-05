using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using HandBookApi.Models;
using HandBookApi.Job;

namespace HandBookApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            //  services.AddDbContext<HandBookContext>(options =>
            // options.UseSqlite(Configuration.GetConnectionString("HandBookContext1")));
            services.AddDbContextPool<HandBookContext>(builder=> builder.UseSqlite(Configuration.GetConnectionString("HandBookContext1")))
            .AddDbContextPool<HandBookSqlServerContext>(builder=> builder.UseSqlServer(Configuration.GetConnectionString("HandBookSqlServerContext"),p => p.UseRowNumberForPaging ()));

            #region  中间件
            // Register the Swagger services
              services.AddSwaggerDocument();
              #endregion
              #region 配置 Cors
              services.AddCors(options =>
        {
            options.AddPolicy("_myAllowSpecificOrigins",
            builder =>
            {
                 builder.AllowAnyOrigin(); //2.可用
            });
        });
      
              #endregion


           //定时任务的注入
           services.AddTransient<Microsoft.Extensions.Hosting.IHostedService, EmailJob>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
#region 启用Cors
 app.UseCors("_myAllowSpecificOrigins"); 
#endregion
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
           #region 
            // Register the Swagger generator and the Swagger UI middlewares
            app.UseOpenApi();
            app.UseSwaggerUi3();
            #endregion
        }
    }
}
