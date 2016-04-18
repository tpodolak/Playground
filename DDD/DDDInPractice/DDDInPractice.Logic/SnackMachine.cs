using System;
using System.Collections.Generic;
using System.Linq;
using static DDDInPractice.Logic.Money;

namespace DDDInPractice.Logic
{
    public class SnackMachine : AggregateRoot
    {
        public virtual Money MoneyInside { get; protected set; } = None;
        public virtual decimal MoneyInTransaction { get; protected set; } = 0m;
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
            MoneyInside += money;
            MoneyInTransaction += money.Amount;
        }

        public virtual void ReturnMoney()
        {
            var money = MoneyInside.Allocate(MoneyInTransaction);
            MoneyInside -= money;
            MoneyInTransaction = 0m;
        }

        public virtual void BuySnack(int position)
        {
            var slot = GetSlot(position);
            if (slot.SnackPile.Price > MoneyInTransaction)
                throw new InvalidOperationException();

            slot.SnackPile = slot.SnackPile.SubtractOne();
            var change = MoneyInside.Allocate(MoneyInTransaction - slot.SnackPile.Price);
            if(change.Amount < MoneyInTransaction - slot.SnackPile.Price)
                throw new InvalidOperationException();
            MoneyInside -= change;
            MoneyInTransaction = 0m;
        }

        public virtual void LoadSnack(int position, SnackPile snackPile)
        {
            var slot = GetSlot(position);
            slot.SnackPile = snackPile;

        }

        public virtual Slot GetSlot(int position)
        {
            return Slots.Single(val => val.Position == position);
        }

        public virtual void LoadMoney(Money dollar)
        {
            MoneyInside += dollar;
        }
    }
}