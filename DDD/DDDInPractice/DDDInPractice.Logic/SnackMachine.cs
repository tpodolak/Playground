using System;
using System.Linq;
using static DDDInPractice.Logic.Money;
namespace DDDInPractice.Logic
{
    public class SnackMachine : Entity
    {
        public Money MoneyInside { get; private set; } = None;
        public Money MoneyInTransaction { get; private set; } = None;

        public void InsertMoney(Money money)
        {
            Money[] allowedCoins = new[] { Cent, TenCent, Quarter, Dollar, FiveDollar, TwentyDollar };
            if (!allowedCoins.Contains(money))
                throw new InvalidOperationException();

            MoneyInTransaction += money;
        }

        public void ReturnMoney()
        {
            MoneyInTransaction = None;
        }

        public void BuySnack()
        {
            MoneyInside += MoneyInTransaction;
            MoneyInTransaction = None;
        }
    }
}


