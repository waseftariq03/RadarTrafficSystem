using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Struc.Interfaces;
using Struc.Models;


namespace Struc.Services
{
    public class TestService : ITestService
    {
        private readonly ILogger<TestService> logger;
        private readonly string testString;
        public TestService(ILogger<TestService> logger, IConfiguration configuration)
        {
            this.logger = logger;
            testString = configuration["Test"]!;
        }

        public async Task<bool> IsTrue(TestModel test)
        {
            try
            {
                //TODO
                logger.LogInformation(testString);

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error...");
                return await Task.FromResult(false);
            }
        }
    }
}
