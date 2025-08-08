using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using DAL.Entities;
using Struc.Interfaces;
using Struc.Models;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace Struc.BackgroundServices

{

    public class TestBGService : BackgroundService

    {

        private readonly ILogger<TestBGService> _logger;
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly IServiceProvider _services;

        public TestBGService(

            ILogger<TestBGService> logger,
            IHostApplicationLifetime appLifetime,
            IServiceProvider services)

        {

            _logger = logger;
            _appLifetime = appLifetime;
            _services = services;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)

        {

            try

            {

                using var scope = _services.CreateScope();
                var radarSimulator = scope.ServiceProvider.GetRequiredService<IRadarSimulator>();
                var violationClassifier = scope.ServiceProvider.GetRequiredService<IViolationClassifier>();
                var violationRepo = scope.ServiceProvider.GetRequiredService<IViolationRepository>();
                var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();
                var random = new Random();


                _logger.LogInformation(" Radar violation monitoring service started.");

                while (!stoppingToken.IsCancellationRequested)

                {

                    var detection = radarSimulator.GenerateDetection();

                    _logger.LogInformation("Detection generated: Plate={Plate}, Speed={Speed}, Direction={Direction}, Lane={Lane}",
                        detection.PlateNumber, detection.Speed, detection.Direction, detection.Lane);

                    var violation = violationClassifier.Classify(detection);

                    if (violation != null)

                    {

                        await violationRepo.SaveAsync(violation);

                        _logger.LogInformation("Violation saved: Type={Type}, Plate={Plate}, Speed={Speed}, Direction={Direction}, Lane={Lane}, Message={Message}",
                            violation.Type, violation.PlateNumber, violation.Speed, violation.Direction, violation.Lane, violation.ViolationMessage);

                        if ((violation.Type == DAL.Entities.ViolationType.Speeding && violation.Speed > 160) ||

                            violation.Type == DAL.Entities.ViolationType.WrongDirection)

                        {

                            _logger.LogWarning(" Critical Violation: Type={Type}, Plate={Plate}, Speed={Speed}, Direction={Direction}, Lane={Lane}",
                                violation.Type, violation.PlateNumber, violation.Speed, violation.Direction, violation.Lane);

                        }

                        notificationService.Notify(violation);

                    }

                    await Task.Delay(TimeSpan.FromSeconds(random.Next(1, 3)), stoppingToken);

                }

            }

            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)

            {

                _logger.LogWarning("Background service is stopping.");

            }

            catch (Exception ex)

            {

                _logger.LogCritical(ex, "Unhandled error in radar detection loop.");
                _appLifetime.StopApplication();

            }

        }

    }

}

