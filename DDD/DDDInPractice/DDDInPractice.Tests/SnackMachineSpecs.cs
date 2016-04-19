using System;
using System.Linq;
using DDDInPractice.Logic;
using DDDInPractice.Logic.SharkedKernel;
using DDDInPractice.Logic.SnackMachines;
using FluentAssertions;
using Xunit;
using static DDDInPractice.Logic.SharkedKernel.Money;
using static DDDInPractice.Logic.SnackMachines.Snack;
namespace DDDInPractice.Tests
{
    public class SnackMachineSpecs
    {
        [Fact]
        public void ReturnMoneyEmptiesMoneyInTransactionTest()
        {
            var snackMachine = new SnackMachine();
            snackMachine.InsertMoney(Cent);
            snackMachine.ReturnMoney();
            snackMachine.MoneyInTransaction.Should().Be(0m);
        }

        [Fact]
        public void InsertMoneyGoesToMoneyInTransactionTest()
        {
            var snacMachine = new SnackMachine();
            snacMachine.InsertMoney(Dollar);
            snacMachine.MoneyInTransaction.Should().Be(Dollar.Amount);
        }

        [Fact]
        public void CannotInsertMoreThanOneCoinOrNoteAtTimeTest()
        {
            var money = Dollar + Cent;
            var snacMachine = new SnackMachine();
            Assert.Throws<InvalidOperationException>(() => snacMachine.InsertMoney(money));
        }

        [Fact]
        public void BuySnackTradesInsertedMoneyForSnack()
        {
            var snackMachine = new SnackMachine();
            snackMachine.LoadSnack(1, new SnackPile(Snack.Chocolate, 10, 1m));
            snackMachine.InsertMoney(Dollar);

            snackMachine.BuySnack(1);

            snackMachine.Slots.Single(val => val.Position == 1).SnackPile.Quantity.Should().Be(9);
            snackMachine.MoneyInTransaction.Should().Be(0m);
            snackMachine.MoneyInside.Amount.Should().Be(1m);
            snackMachine.GetSnackPile(1).Quantity.Should().Be(9);
        }

        [Fact]
        public void CannotMakePurchaseWhenPriceGreaterThanMoneyInTransactionTest()
        {
            var snackMachine = new SnackMachine();
            snackMachine.InsertMoney(Dollar);
            snackMachine.LoadSnack(1, new SnackPile(Chocolate, 1, 2));
            Assert.Throws<InvalidOperationException>(() => snackMachine.BuySnack(1));
        }

        [Fact]
        public void ReturnMoneyWithHighetDenominationFirstTest()
        {
            var snackMachine = new SnackMachine();
            snackMachine.LoadMoney(Dollar);

            snackMachine.InsertMoney(Quarter);
            snackMachine.InsertMoney(Quarter);
            snackMachine.InsertMoney(Quarter);
            snackMachine.InsertMoney(Quarter);
            snackMachine.ReturnMoney();
            snackMachine.MoneyInside.QuarterCount.Should().Be(4);
            snackMachine.MoneyInside.OneDollarCount.Should().Be(0);
        }

        [Fact]
        public void ChangeIsReturnedAfterPurchaseTest()
        {
            var snackMachine = new SnackMachine();
            snackMachine.LoadSnack(1, new SnackPile(Chocolate, 1, 0.5m));
            snackMachine.LoadMoney(new Money(0, 10, 0, 0, 0, 0));
            snackMachine.InsertMoney(Dollar);
            snackMachine.BuySnack(1);
            snackMachine.MoneyInside.Amount.Should().Be(1.5m);
            snackMachine.MoneyInTransaction.Should().Be(0m);
        }
    }
}