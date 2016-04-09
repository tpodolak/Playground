namespace DDDInPractice.Logic
{
	public class Money
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
	}
}