namespace DDDInPractice.Logic
{
	public class SnackMachine : Entity
	{
		public Money MoneyInside { get; set; }
		public Money MoneyInTransaction { get; set; }

		public void InsertMoney(Money money)
		{
			MoneyInTransaction += money;
		}

		public void ReturnMoney()
		{
			// MoneyInTransaction = 0x0;
		}

		public void BuySnack()
		{
			MoneyInside += MoneyInTransaction;
		}
	}
}


