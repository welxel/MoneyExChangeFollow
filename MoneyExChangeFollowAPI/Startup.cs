using AppCore.Business.Services.Bases;
using Business.Services;
using Business.Services.Bases;
using DataAccess.EntityFramework.Contexts;
using DataAccess.EntityFramework.Repositories;
using DataAccess.EntityFramework.Repositories.Bases;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MoneyExChangeFollowAPI.Tasking;
using Newtonsoft.Json.Serialization;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyExChangeFollowAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver =
                                             new DefaultContractResolver();
            });
            services.AddScoped<DbContext, ETradeContext>();

            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();

            services.AddScoped<ICurrencyDetailService, CurrencyDetailService>();
            services.AddScoped<ICurrencyDetailRepository, CurrencyDetailRepository>();

            var jobKey = new JobKey("notificationJob");
            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();
                q.SchedulerId = "JobScheduler";
                q.SchedulerName = "Job Scheduler";
                q.AddJob<FillCurrentDetail>(j => j.WithIdentity(jobKey));
                q.AddTrigger(t => t
                   .WithIdentity("currencyRecurringJob")
                   .ForJob(jobKey)
                   .StartNow()
                   .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(00, 17))
                );
            });
            services.AddQuartzHostedService();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Protel Currency API",
                    Version = "v1",
                    Description = "TCMB Currency following api.",
                    Contact = new OpenApiContact
                    {
                        Name = "Furkan BEKTAS",
                        Email = "furkaanbektas@gmail.com",
                        Url = new Uri("https://furkanbektas.net"),
                    },
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger(c => c.SerializeAsV2 = true); ;

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Protel API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();
            

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
