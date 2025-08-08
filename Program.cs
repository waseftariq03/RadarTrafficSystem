using DAL.DB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Struc.BackgroundServices;
using Struc.Interfaces;
using Struc.Models;
using Struc.Services;
using Microsoft.EntityFrameworkCore;


var builder = Host.CreateDefaultBuilder(args)

.ConfigureAppConfiguration((hostingContext, config) =>

{
   config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

})

.UseSerilog((hostingContext, services, loggerConfiguration) =>

{

   loggerConfiguration
       .ReadFrom.Configuration(hostingContext.Configuration) 
       .WriteTo.Console()
       .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day);

})



   .ConfigureServices((context, services) =>

        {
            services.AddDbContext<DatabaseContext>(options =>
            options.UseSqlServer(context.Configuration.GetConnectionString("ConStr")));


            services.Configure<ViolationSettings>(context.Configuration.GetSection("ViolationSettings"));
            services.AddSingleton<ITestService, TestService>();
            services.AddSingleton<IRadarSimulator, RadarSimulator>();
            services.AddSingleton<IViolationClassifier, ViolationClassifier>();
            services.AddSingleton<IViolationRepository, ViolationRepository>();
            services.AddSingleton<INotificationService, NotificationService>();
            services.AddHostedService<TestBGService>();

        });

            var host = builder.Build();
            await host.RunAsync();




/*using (var scope = host.Services.CreateScope())

{

    var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
     

}*/
