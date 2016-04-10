using System;
using System.Collections.Generic;
using System.Linq;
using static DDDInPractice.Logic.Money;

namespace DDDInPractice.Logic
{
    public class SnackMachine : Entity
    {
        public virtual Money MoneyInside { get; protected set; } = None;
        public virtual Money MoneyInTransaction { get; protected set; } = None;
        public virtual IList<Slot> Slots { get; protected set; }

        public SnackMachine()
        {
            Slots = new List<Slot>
            {
                new Slot(this, 1, null, 0, 0),
                new Slot(this, 2, null, 0, 0),
                new Slot(this, 3, null, 0, 0)
            };
        }

        public virtual void InsertMoney(Money money)
        {
            Money[] allowedCoins = new[] {Cent, TenCent, Quarter, Dollar, FiveDollar, TwentyDollar};
            if (!allowedCoins.Contains(money))
                throw new InvalidOperationException();

            MoneyInTransaction += money;
        }

        public virtual void ReturnMoney()
        {
            MoneyInTransaction = None;
        }

        public virtual void BuySnack(int position)
        {
            var slot = Slots.Single(val => val.Position == position);
            slot.Quantity--;
            MoneyInside += MoneyInTransaction;
            MoneyInTransaction = None;
        }

        public virtual void LoadSnack(int position, Snack snack, int quantity, decimal price)
        {
            var slot = Slots.SingleOrDefault(val => val.Position == position);
            slot.Snack = snack;
            slot.Price = price;
            slot.Quantity = quantity;
        }
    }
}