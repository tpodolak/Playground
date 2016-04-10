using System;
using System.Collections.Generic;
using System.Linq;
using static DDDInPractice.Logic.Money;

namespace DDDInPractice.Logic
{
    public class SnackMachine : AggregateRoot
    {
        public virtual Money MoneyInside { get; protected set; } = None;
        public virtual Money MoneyInTransaction { get; protected set; } = None;
        public virtual IList<Slot> Slots { get; protected set; }

        public SnackMachine()
        {
            Slots = new List<Slot>
            {
                new Slot(this, 1),
                new Slot(this, 2),
                new Slot(this, 3)
            };
        }

        public virtual SnackPile GetSnackPile(int position)
        {
            return GetSlot(position).SnackPile;
        }

        public virtual void InsertMoney(Money money)
        {
            Money[] allowedCoins = new[] { Cent, TenCent, Quarter, Dollar, FiveDollar, TwentyDollar };
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
            var slot = GetSlot(position);
            slot.SnackPile = slot.SnackPile.SubtractOne();
            MoneyInside += MoneyInTransaction;
            MoneyInTransaction = None;
        }

        public virtual void LoadSnack(int position, SnackPile snackPile)
        {
            var slot = GetSlot(position);
            slot.SnackPile = snackPile;

        }

        public Slot GetSlot(int position)
        {
            return Slots.Single(val => val.Position == position);
        }
    }
}