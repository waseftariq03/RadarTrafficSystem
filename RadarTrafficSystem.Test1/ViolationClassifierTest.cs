using Struc.Models;
using Struc.Services;
using DAL.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Xunit;
using Moq;

namespace RadarTrafficSystem.Tests
{
    public class ViolationClassifierTests
    {
        private readonly ViolationClassifier _classifier;

        public ViolationClassifierTests()
        {
            var settings = new ViolationSettings
            {
                MaxAllowedSpeed = 120,
                AllowedDirections = new List<string> { "North", "East" },
                EmergencyLanes = new List<int> { 4 }
            };

            var mockOptions = Mock.Of<IOptions<ViolationSettings>>(opt => opt.Value == settings);
            var mockLogger = Mock.Of<ILogger<ViolationClassifier>>();

            _classifier = new ViolationClassifier(mockOptions, mockLogger);
        }

        [Fact]
        public void Classify_ShouldDetectSpeeding()
        {
            var detection = new Violation
            {
                Speed = 150,
                Direction = "North",
                Lane = 1
            };

            var result = _classifier.Classify(detection);
            Assert.NotNull(result);
            Assert.Equal(ViolationType.Speeding, result.Type);
        }

        [Fact]
        public void Classify_ShouldDetectWrongDirection()
        {
            var detection = new Violation
            {
                Speed = 100,
                Direction = "West",
                Lane = 1
            };

            var result = _classifier.Classify(detection);
            Assert.NotNull(result);
            Assert.Equal(ViolationType.WrongDirection, result.Type);
        }

        [Fact]
        public void Classify_ShouldDetectLaneViolation()
        {
            var detection = new Violation
            {
                Speed = 100,
                Direction = "North",
                Lane = 4
            };

            var result = _classifier.Classify(detection);
            Assert.NotNull(result);
            Assert.Equal(ViolationType.LaneViolation, result.Type);
        }

        [Fact]
        public void Classify_ShouldReturnNull_WhenNoViolation()
        {
            var detection = new Violation
            {
                Speed = 100,
                Direction = "North",
                Lane = 2
            };

            var result = _classifier.Classify(detection);
            Assert.Null(result);
        }
    }
}
