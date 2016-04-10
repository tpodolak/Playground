namespace DDDInPractice.Logic
{
	public class Money : ValueObject<Money>
	{
		public int OneCentCount { get; set; }
		public int TenCentCount { get; set; }
		public int QuaterCount { get; set; }
		public int OneDollarCount { get; set; }
		public int FiveDollarCount { get; set; }
		public int TwentyDollarCount { get; set; }

		public Money(int oneCentCount, int tenCentCount, int quaterCount, int oneDollarCount, int fiveDollarCount, int twentyDollarCount)
		{
			OneCentCount = oneCentCount;
			TenCentCount = tenCentCount;
			QuaterCount = quaterCount;
			OneDollarCount = oneDollarCount;
			FiveDollarCount = fiveDollarCount;
			TwentyDollarCount = twentyDollarCount;
		}

		public static Money operator +(Money left, Money right)
		{
			return new Money(left.OneCentCount + right.OneCentCount, left.TenCentCount + right.TenCentCount, left.QuaterCount + right.QuaterCount,
				left.OneDollarCount + right.OneDollarCount, left.FiveDollarCount + right.FiveDollarCount, left.TwentyDollarCount + right.TwentyDollarCount);
		}

		protected override int GetHashCodeInternal()
		{
			unchecked
			{
				int hashCode = base.GetHashCode();
				hashCode = (hashCode*397) ^ OneCentCount;
				hashCode = (hashCode*397) ^ TenCentCount;
				hashCode = (hashCode*397) ^ QuaterCount;
				hashCode = (hashCode*397) ^ OneDollarCount;
				hashCode = (hashCode*397) ^ FiveDollarCount;
				hashCode = (hashCode*397) ^ TwentyDollarCount;
				return hashCode;
			}
		}

		protected override bool EqualsInternal(Money other)
		{
			return OneCentCount == other.OneCentCount && TenCentCount == other.TenCentCount && QuaterCount == other.QuaterCount &&
			       OneDollarCount == other.OneDollarCount && FiveDollarCount == other.FiveDollarCount && TwentyDollarCount == other.TwentyDollarCount;
		}

	}
}