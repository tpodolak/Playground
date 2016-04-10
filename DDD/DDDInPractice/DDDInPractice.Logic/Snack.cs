namespace DDDInPractice.Logic
{
	public class Snack : Entity
	{
		public virtual string Name { get; set; }

		private Snack()
		{

		}

		public Snack(string name)
		{
			Name = name;
		}
	}
}