using Microsoft.Graph;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using SCJ.Booking.MVC.Extensions;

namespace SCJ.Booking.UnitTest
{
    public class FairUseAlgorithmTests
    {
        public FairUseAlgorithmTests()
        {

        }

        [Fact]
        public void FairUseAlgorithmTestSuccess()
        {
            //create sequential list to perform lottery on
            List<int> controlList = Enumerable.Range(1, 100).ToList();
            List<int> firstLotteryList = Enumerable.Range(1, 100).ToList();
            List<int> secondLotteryList = Enumerable.Range(1, 100).ToList();

            Assert.True(controlList.SequenceEqual(firstLotteryList));
            Assert.True(controlList.SequenceEqual(secondLotteryList));

            //perform lottery
            firstLotteryList.Shuffle();
            secondLotteryList.Shuffle();

            //check that the lotteryList has been shuffled
            //odds of the shuffle returning exactly 1-100 seqentially is 9.332622e+157 so extremely unlikely but a possibility
            Assert.False(firstLotteryList.SequenceEqual(controlList));

            //check that the lottery is indeed random by checking the 2 lottery lists against each other
            //again, there is a possibility that they both are the same but extremely extremely unlikely
            Assert.False(firstLotteryList.SequenceEqual(secondLotteryList));
        }
    }
}
