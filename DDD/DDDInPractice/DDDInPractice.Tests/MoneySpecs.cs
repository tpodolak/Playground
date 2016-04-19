using System;
using DDDInPractice.Logic;
using DDDInPractice.Logic.SharkedKernel;
using FluentAssertions;
using Xunit;

namespace DDDInPractice.Tests
{
    public class MoneySpecs
    {
        [Fact]
        public void SumOfTwoMoneysProducesCorrectResultTest()
        {
            var left = new Money(1, 2, 3, 4, 5, 6);
            var right = new Money(1, 2, 3, 4, 5, 6);
            var result = left + right;
            result.OneCentCount.Should().Be(2);
            result.TenCentCount.Should().Be(4);
            result.QuarterCount.Should().Be(6);
            result.OneDollarCount.Should().Be(8);
            result.FiveDollarCount.Should().Be(10);
            result.TwentyDollarCount.Should().Be(12);
        }

        [Fact]
        public void TwoMoneyInstancesEqualWhenContainsTheSameMoneyAmountTest()
        {
            var left = new Money(1, 2, 3, 4, 5, 6);
            var right = new Money(1, 2, 3, 4, 5, 6);
            left.Should().Be(right);
            left.GetHashCode().Should().Be(right.GetHashCode());
        }

        [Fact]
        public void TwoMoneyInstanceDoNotEqualWhenContainsDifferentMoneyAmmount()
        {
            var left = new Money(1, 0, 0, 0, 0, 0);
            var right = new Money(0, 1, 0, 0, 0, 0);
            left.Should().NotBe(right);
            left.GetHashCode().Should().NotBe(right.GetHashCode());
        }

        [Theory]
        [InlineData(-1, 0, 0, 0, 0, 0)]
        [InlineData(0, -2, 0, 0, 0, 0)]
        [InlineData(0, 0, -3, 0, 0, 0)]
        [InlineData(0, 0, 0, -4, 0, 0)]
        [InlineData(0, 0, 0, 0, -5, 0)]
        [InlineData(0, 0, 0, 0, 0, -6)]
        public void CannotCreateMoneyWithNegativeValue(int oneCentCount, int tenCentCount, int quarterCount, int oneDollarCount, int fiveDollarCount,
            int twentyDollarCount)
        {
            Assert.Throws<ArgumentException>(
                () => new Money(oneCentCount, tenCentCount, quarterCount, oneDollarCount, fiveDollarCount, twentyDollarCount));
        }

        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0, 0)]
        [InlineData(1, 0, 0, 0, 0, 0, 0.01)]
        [InlineData(1, 2, 0, 0, 0, 0, 0.21)]
        [InlineData(1, 2, 3, 0, 0, 0, 0.96)]
        [InlineData(1, 2, 3, 4, 0, 0, 4.96)]
        [InlineData(1, 2, 3, 4, 5, 0, 29.96)]
        [InlineData(1, 2, 3, 4, 5, 6, 149.96)]
        [InlineData(11, 0, 0, 0, 0, 0, 0.11)]
        [InlineData(110, 0, 0, 0, 100, 0, 501.1)]
        public void AmountIsCalculatedCorrectlyTest(int oneCentCount, int tenCentCount, int quarterCount, int oneDollarCount, int fiveDollarCount,
            int twentyDollarCount, double expectedAmount)
        {
            var money = new Money(
                oneCentCount,
                tenCentCount,
                quarterCount,
                oneDollarCount,
                fiveDollarCount,
                twentyDollarCount);

            money.Amount.ShouldBeEquivalentTo(expectedAmount);
        }

        [Fact]
        public void Subtraction_of_two_moneys_produces_correct_result()
        {
            var money1 = new Money(10, 10, 10, 10, 10, 10);
            var money2 = new Money(1, 2, 3, 4, 5, 6);

            var result = money1 - money2;

            result.OneCentCount.Should().Be(9);
            result.TenCentCount.Should().Be(8);
            result.QuarterCount.Should().Be(7);
            result.OneDollarCount.Should().Be(6);
            result.FiveDollarCount.Should().Be(5);
            result.TwentyDollarCount.Should().Be(4);
        }

        [Fact]
        public void Cannot_subtract_more_than_exists()
        {
            var left = new Money(1, 0, 0, 0, 0, 0);
            var right = new Money(0, 1, 0, 0, 0, 0);
            Assert.Throws<InvalidOperationException>(() => left - right);
        }
    }
}


