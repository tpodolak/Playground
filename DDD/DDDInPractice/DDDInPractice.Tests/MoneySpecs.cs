using System;
using DDDInPractice.Logic;
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
    }
}


