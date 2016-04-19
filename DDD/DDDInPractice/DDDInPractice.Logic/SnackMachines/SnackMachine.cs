using System;
using System.Collections.Generic;
using System.Linq;
using DDDInPractice.Logic.Common;
using DDDInPractice.Logic.SharkedKernel;

namespace DDDInPractice.Logic.SnackMachines
{
    public class SnackMachine : AggregateRoot
    {
        public virtual Money MoneyInside { get; protected set; } = Money.None;
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
            Money[] allowedCoins = new[] { Money.Cent, Money.TenCent, Money.Quarter, Money.Dollar, Money.FiveDollar, Money.TwentyDollar };
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
        public virtual string CanBuySnack(int position)
        {
            SnackPile snackPile = GetSnackPile(position);

            if (snackPile.Quantity == 0)
                return "The snack pile is empty";

            if (MoneyInTransaction < snackPile.Price)
                return "Not enough money";

            if (!MoneyInside.CanAllocate(MoneyInTransaction - snackPile.Price))
                return "Not enough change";

            return string.Empty;
        }

        public virtual IReadOnlyList<SnackPile> GetAllSnackPiles()
        {
            return Slots
                .OrderBy(x => x.Position)
                .Select(x => x.SnackPile)
                .ToList();
        }
    }
}