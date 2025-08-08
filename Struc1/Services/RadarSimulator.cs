using DAL.Entities;
using Struc.Interfaces;
using Struc.Models;

namespace Struc.Services

{

    public class RadarSimulator : IRadarSimulator

    {

        private readonly Random _random = new();

        public Violation GenerateDetection()

        {

            var directions = new[] { "North", "East", "South", "West" }; 
            var lanes = new[] { 1, 2, 3, 4 }; 

            return new Violation

            {

                PlateNumber = $"PLT-{_random.Next(1000, 9999)}",
                Speed = _random.Next(60, 180), 
                Timestamp = DateTime.UtcNow,
                Lane = lanes[_random.Next(lanes.Length)], 
                Direction = directions[_random.Next(directions.Length)], 
                LocationX = _random.NextDouble() * 100,
                LocationY = _random.NextDouble() * 100

            };

        }

    }

}

