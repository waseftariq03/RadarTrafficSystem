using DAL.Entities;
using Microsoft.Extensions.Logging;
using Struc.Interfaces;

namespace Struc.Services

{

    public class NotificationService : INotificationService

    {

        private readonly ILogger<NotificationService> _logger;

        public NotificationService(ILogger<NotificationService> logger)

        {

            _logger = logger;

        }

        public void Notify(Violation violation)

        {

            if (violation.Type == ViolationType.Speeding)

                _logger.LogWarning("Speeding Violation: {Message}", violation.ViolationMessage);

            else if (violation.Type == ViolationType.WrongDirection)

                _logger.LogWarning("Wrong Direction Violation: {Message}", violation.ViolationMessage);

            else

                _logger.LogInformation("Violation: {Message}", violation.ViolationMessage);

        }

    }

}

