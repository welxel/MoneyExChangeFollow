using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyExChangeFollowAPI.Tasking
{
    public class SchedulerTask
    {
        public static async System.Threading.Tasks.Task StartAsync()
        {
            try
            {
                var scheduler = await StdSchedulerFactory.GetDefaultScheduler();
                if (!scheduler.IsStarted)
                {
                    await scheduler.Start();
                }
                var job1 = JobBuilder.Create<FillCurrentDetail>().Build();
                var trigger1 = TriggerBuilder.Create().WithDailyTimeIntervalSchedule((s) => { s.OnEveryDay().StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(21, 58)); }).StartNow().Build();
                await scheduler.ScheduleJob(job1, trigger1);
            }
            catch (Exception ex) { }
        }
    }
}
