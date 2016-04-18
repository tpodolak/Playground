namespace DDDInPractice.Logic
{
	public class Snack : Entity
	{
		public virtual string Name { get; set; }

		public Snack()
		{

		}

		public Snack(string name)
		{
			Name = name;
		}
	}
}