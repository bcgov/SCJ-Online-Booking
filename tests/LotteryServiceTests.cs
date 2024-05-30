using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SCJ.Booking.Data;
using SCJ.Booking.UnitTest.Helpers;
using Xunit;

namespace SCJ.Booking.UnitTest
{
    public class LotteryServiceTests
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _dbContext;

        public LotteryServiceTests()
        {
            _configuration = TestUtils.GetConfiguration();
            _dbContext = DatabaseUtils.GetDbContext(_configuration);

            string testFile = $"Data{Path.DirectorySeparatorChar}FairUseTestRequests.csv";
            DatabaseUtils.LoadCsvData(_dbContext, testFile);
        }

        [Fact]
        public async Task RunLottery()
        {
            var service = new TaskRunner.Services.LotteryService(_configuration, _dbContext);

            bool moreWorkRemaining = true;
            while (moreWorkRemaining)
            {
                moreWorkRemaining = await service.RunNextLotteryStep();
            }
        }
    }
}
