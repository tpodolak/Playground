using System;

namespace DDDInPractice.Logic
{
    public class Money : ValueObject<Money>
    {
        public int OneCentCount { get; set; }
        public int TenCentCount { get; set; }
        public int QuarterCount { get; set; }
        public int OneDollarCount { get; set; }
        public int FiveDollarCount { get; set; }
        public int TwentyDollarCount { get; set; }

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

    }
}