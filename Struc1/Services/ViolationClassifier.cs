using DAL.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Struc.Interfaces;
using Struc.Models;

namespace Struc.Services

{

    public class ViolationClassifier : IViolationClassifier

    {

        private readonly ViolationSettings _settings;
        private readonly ILogger<ViolationClassifier> _logger;

        public ViolationClassifier(IOptions<ViolationSettings> options, ILogger<ViolationClassifier> logger)

        {
            _settings = options.Value;
            _logger = logger;
        }

        public Violation Classify(Violation detection)

        {

            if (detection.Speed > _settings.MaxAllowedSpeed)

            {

                return new Violation

                {

                    Type = DAL.Entities.ViolationType.Speeding,
                    ViolationMessage = "Speeding Detected",
                    PlateNumber = detection.PlateNumber,
                    Speed = detection.Speed,
                    Timestamp = detection.Timestamp,
                    Lane = detection.Lane,
                    Direction = detection.Direction,
                    LocationX = detection.LocationX,
                    LocationY = detection.LocationY

                };

            }

            if (!_settings.AllowedDirections.Contains(detection.Direction))

            {

                return new Violation

                {

                    Type = DAL.Entities.ViolationType.WrongDirection,
                    ViolationMessage = "Wrong Direction",
                    PlateNumber = detection.PlateNumber,
                    Speed = detection.Speed,
                    Timestamp = detection.Timestamp,
                    Lane = detection.Lane,
                    Direction = detection.Direction,
                    LocationX = detection.LocationX,
                    LocationY = detection.LocationY

                };

            }

            if (_settings.EmergencyLanes.Contains(detection.Lane))

            {

                return new Violation

                {

                    Type = DAL.Entities.ViolationType.LaneViolation,
                    ViolationMessage = "Emergency Lane Violation",
                    PlateNumber = detection.PlateNumber,
                    Speed = detection.Speed,
                    Timestamp = detection.Timestamp,
                    Lane = detection.Lane,
                    Direction = detection.Direction,
                    LocationY = detection.LocationX,
                    LocationX = detection.LocationY
                };

            }

            return null!;

        }


    }



}

