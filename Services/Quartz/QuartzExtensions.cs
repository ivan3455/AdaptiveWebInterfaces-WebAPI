using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace AdaptiveWebInterfaces_WebAPI.Services.Quartz
{
    public static class QuartzExtensions
    {
        public static void AddQuartzServices(this IServiceCollection services)
        {
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            services.AddSingleton<EmailJob>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(EmailJob),
                cronExpression: "0 0/10 * * * ?")); // every 10 minutes

            services.AddHostedService<QuartzHostedService>();
        }
    }
}
