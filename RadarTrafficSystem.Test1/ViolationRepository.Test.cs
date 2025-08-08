using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using DAL.DB;
using DAL.Entities;
using Struc.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace RadarTrafficSystem.Tests

{

    public class ViolationRepositoryTests

    {

        [Fact]

        public async Task SaveAsync_ShouldAddViolationToDatabase()

        {

            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;


            var mockLogger = new Mock<ILogger<ViolationRepository>>();

            using var context = new DatabaseContext(options);
            var repo = new ViolationRepository(context, mockLogger.Object);

            var violation = new Violation

            {

                PlateNumber = "TEST123",
                Speed = 120,
                Direction = "North",
                Lane = 2,
                Type = ViolationType.Speeding,
                Timestamp = DateTime.UtcNow,
                ViolationMessage = "Speeding Detected"

            };

            

            await repo.SaveAsync(violation);

            

            var saved = await context.Violations

                .AsNoTracking().FirstOrDefaultAsync(v => v.PlateNumber == "TEST123");

            Assert.NotNull(saved);
            Assert.Equal(120, saved.Speed);
            Assert.Equal("Speeding Detected", saved.ViolationMessage);
            Assert.Equal(ViolationType.Speeding, saved.Type);

        }

    }

}

