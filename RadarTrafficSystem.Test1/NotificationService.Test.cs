using DAL.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using Struc.Services;
using Xunit;

namespace RadarTrafficSystem.Tests
{
    public class NotificationServiceTests
    {
        [Theory]
        [InlineData(ViolationType.Speeding, "Speeding violation detected!")]
        [InlineData(ViolationType.WrongDirection, "Wrong direction violation!")]
        [InlineData(ViolationType.LaneViolation, "Emergency lane violation!")]

        public void Notify_LogsCorrectMessage(ViolationType type, string expectedMessage)
        {
            
            var mockLogger = new Mock<ILogger<NotificationService>>();
            var service = new NotificationService(mockLogger.Object);

            var violation = new Violation
            {
                Type = type,
                ViolationMessage = expectedMessage
            };

            
            service.Notify(violation);        
            
        }
    }
}
