namespace DDDInPractice.Logic.Common
{
	public abstract class ValueObject<T> where T:ValueObject<T>
	{
		public override bool Equals(object obj)
		{
			var valueObject = obj as T;
			return !ReferenceEquals(valueObject, null) && EqualsInternal(valueObject);
		}

		public override int GetHashCode()
		{
			return GetHashCodeInternal();
		}

		protected abstract int GetHashCodeInternal();

		protected abstract bool EqualsInternal(T other);

		public static bool operator ==(ValueObject<T> left, ValueObject<T> right)
		{
			if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
				return true;

			if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
				return false;

			return left.Equals(right);
		}

		public static bool operator !=(ValueObject<T> left, ValueObject<T> right)
		{
			return !(left == right);
		}
	}
}