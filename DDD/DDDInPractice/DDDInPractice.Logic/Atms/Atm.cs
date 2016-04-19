using System;
using DDDInPractice.Logic.Common;
using DDDInPractice.Logic.SharedKernel;
using static DDDInPractice.Logic.SharedKernel.Money;

namespace DDDInPractice.Logic.Atms
{
    public class Atm : AggregateRoot
    {
        private const decimal CommissionRate = 0.01m;

        public virtual Money MoneyInside { get; protected set; } = None;
        public virtual decimal MoneyCharged { get; protected set; }

        public virtual string CanTakeMoney(decimal amount)
        {
            if (amount <= 0m)
                return "Invalid amount";

            if (MoneyInside.Amount < amount)
                return "Not enough money";

            if (!MoneyInside.CanAllocate(amount))
                return "Not enough change";

            return string.Empty;
        }

        public virtual void TakeMoney(decimal amount)
        {
            if (CanTakeMoney(amount) != string.Empty)
                throw new InvalidOperationException();

            Money output = MoneyInside.Allocate(amount);
            MoneyInside -= output;

            decimal amountWithCommission = CalculateAmountWithCommission(amount);
            MoneyCharged += amountWithCommission;
        }

        public virtual decimal CalculateAmountWithCommission(decimal amount)
        {
            decimal commission = amount * CommissionRate;
            decimal lessThanCent = commission % 0.01m;
            if (lessThanCent > 0)
            {
                commission = commission - lessThanCent + 0.01m;
            }
            return amount + commission;
        }

        public virtual void LoadMoney(Money money)
        {
            MoneyInside += money;
        }
    }
}