using System;

namespace DDDInPractice.Logic
{
    public class Money : ValueObject<Money>
    {
        public static Money None { get; } = new Money(0, 0, 0, 0, 0, 0);
        public static Money Cent { get; } = new Money(1, 0, 0, 0, 0, 0);
        public static Money TenCent { get; } = new Money(0, 1, 0, 0, 0, 0);
        public static Money Quarter { get; } = new Money(0, 0, 1, 0, 0, 0);
        public static Money Dollar { get; } = new Money(0, 0, 0, 1, 0, 0);
        public static Money FiveDollar { get; } = new Money(0, 0, 0, 0, 1, 0);
        public static Money TwentyDollar { get; } = new Money(0, 0, 0, 0, 0, 1);

        public int OneCentCount { get; }
        public int TenCentCount { get; }
        public int QuarterCount { get; }
        public int OneDollarCount { get; }
        public int FiveDollarCount { get; }
        public int TwentyDollarCount { get; }

        public decimal Amount
            => OneCentCount * 0.01m + TenCentCount * 0.1m + QuarterCount * 0.25m + OneDollarCount + FiveDollarCount * 5 + TwentyDollarCount * 20;

        private Money()
        {
            
        }

        public Money(int oneCentCount, int tenCentCount, int quarterCount, int oneDollarCount, int fiveDollarCount, int twentyDollarCount)
        {
            const string msg = "Value cannot be negative";
            if (oneCentCount < 0)
                throw new ArgumentException(msg, nameof(oneCentCount));
            if (tenCentCount < 0)
                throw new ArgumentException(msg, nameof(tenCentCount));
            if (quarterCount < 0)
                throw new ArgumentException(msg, nameof(quarterCount));
            if (oneDollarCount < 0)
                throw new ArgumentException(msg, nameof(oneDollarCount));
            if (fiveDollarCount < 0)
                throw new ArgumentException(msg, nameof(fiveDollarCount));
            if (twentyDollarCount < 0)
                throw new ArgumentException(msg, nameof(twentyDollarCount));


            OneCentCount = oneCentCount;
            TenCentCount = tenCentCount;
            QuarterCount = quarterCount;
            OneDollarCount = oneDollarCount;
            FiveDollarCount = fiveDollarCount;
            TwentyDollarCount = twentyDollarCount;
        }

        public static Money operator +(Money left, Money right)
        {
            return new Money(left.OneCentCount + right.OneCentCount, left.TenCentCount + right.TenCentCount, left.QuarterCount + right.QuarterCount,
                left.OneDollarCount + right.OneDollarCount, left.FiveDollarCount + right.FiveDollarCount, left.TwentyDollarCount + right.TwentyDollarCount);
        }

        public static Money operator -(Money left, Money right)
        {
            if (left.Amount < right.Amount)
                throw new InvalidOperationException();

            return new Money(left.OneCentCount - right.OneCentCount, left.TenCentCount - right.TenCentCount, left.QuarterCount - right.QuarterCount,
                left.OneDollarCount - right.OneDollarCount, left.FiveDollarCount - right.FiveDollarCount, left.TwentyDollarCount - right.TwentyDollarCount);
        }

        protected override int GetHashCodeInternal()
        {
            unchecked
            {
                int hashCode = OneCentCount.GetHashCode();
                hashCode = (hashCode * 397) ^ OneCentCount;
                hashCode = (hashCode * 397) ^ TenCentCount;
                hashCode = (hashCode * 397) ^ QuarterCount;
                hashCode = (hashCode * 397) ^ OneDollarCount;
                hashCode = (hashCode * 397) ^ FiveDollarCount;
                hashCode = (hashCode * 397) ^ TwentyDollarCount;
                return hashCode;
            }
        }

        protected override bool EqualsInternal(Money other)
        {
            return OneCentCount == other.OneCentCount && TenCentCount == other.TenCentCount && QuarterCount == other.QuarterCount &&
                   OneDollarCount == other.OneDollarCount && FiveDollarCount == other.FiveDollarCount && TwentyDollarCount == other.TwentyDollarCount;
        }

        public override string ToString()
        {
            if (Amount < 1)
                return "¢" + (Amount * 100).ToString("0");

            return "$" + Amount.ToString("0.00");
        }
    }
}