using System.Runtime.Remoting.Messaging;

namespace DDDInPractice.Logic
{
	public class Entity
	{
		public virtual int Id { get; private set; }



		protected bool Equals(Entity other)
		{
			return Id == other.Id;
		}

		public override bool Equals(object obj)
		{
			var other = obj as Entity;

			if (ReferenceEquals(null, other)) return false;

			if (ReferenceEquals(this, other)) return true;

			if (obj.GetType() != this.GetType()) return false;

			if (Id == 0 && other.Id == 0)
				return false;

			return Equals((Entity) obj);
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}

		public static bool operator ==(Entity left, Entity right)
		{
			if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
				return true;

			if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
				return false;

			return left.Equals(right);
		}

		public static bool operator !=(Entity left, Entity right)
		{
			return !(left == right);
		}
	}
}