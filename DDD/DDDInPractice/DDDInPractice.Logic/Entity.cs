using System;
using NHibernate.Proxy;

namespace DDDInPractice.Logic
{
	public class Entity
	{
		public virtual int Id { get; protected set; }

		protected bool Equals(Entity other)
		{
			return Id == other.Id;
		}

		public override bool Equals(object obj)
		{
			var other = obj as Entity;

			if (ReferenceEquals(null, other)) return false;

			if (ReferenceEquals(this, other)) return true;

			if (other.GetRealType() != this.GetRealType()) return false;

			if (Id == 0 && other.Id == 0)
				return false;

			return Equals((Entity) obj);
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

	    public override int GetHashCode()
	    {
	        return (this.GetRealType().ToString() + Id).GetHashCode();
	    }

	    private Type GetRealType()
	    {
	        return NHibernateProxyHelper.GetClassWithoutInitializingProxy(this);
	    }
	}
}