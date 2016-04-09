using System;

namespace DDDInPractice.Logic
{
	public class SnackMachine
	{
		public Money MoneyInside { get; set; }
		public Money MoneyInTransaction { get; set; }

		public void InsertMoney(Money money)
		{
			MoneyInTransaction += money;
		}

		public void BuySnack()
		{
			MoneyInside += MoneyInTransaction;
		}

		public void ReturnMoney()
		{
			// MoneyInTransaction = 0x0;
		}
	}
}


