using System;
using System.Linq;
using DDDInPractice.Logic;
using FluentAssertions;
using Xunit;
using static DDDInPractice.Logic.Money;

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
            snackMachine.MoneyInTransaction.Amount.Should().Be(0m);
        }

        [Fact]
        public void InsertMoneyGoesToMoneyInTransactionTest()
        {
            var snacMachine = new SnackMachine();
            snacMachine.InsertMoney(Dollar);
            snacMachine.MoneyInTransaction.Amount.Should().Be(Dollar.Amount);
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
            snackMachine.LoadSnack(1, new SnackPile(new Snack("Snickers"), 10, 1m));
            snackMachine.InsertMoney(Dollar);

            snackMachine.BuySnack(1);

            snackMachine.Slots.Single(val => val.Position == 1).SnackPile.Quantity.Should().Be(9);
            snackMachine.MoneyInTransaction.Should().Be(None);
            snackMachine.MoneyInside.Amount.Should().Be(1m);
            snackMachine.GetSnackPile(1).Quantity.Should().Be(9);
        }
    }
}