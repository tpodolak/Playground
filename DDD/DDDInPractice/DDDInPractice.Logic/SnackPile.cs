namespace DDDInPractice.Logic
{
    public class SnackPile : ValueObject<SnackPile>
    {
        public virtual Snack Snack { get; }
        public virtual int Quantity { get; }
        public virtual decimal Price { get; }

        private SnackPile()
        {

        }

        public SnackPile(Snack snack, int quantity, decimal price)
        {
            Snack = snack;
            Quantity = quantity;
            Price = price;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = base.GetHashCode();
                hashCode = (hashCode * 397) ^ (Snack != null ? Snack.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Quantity;
                hashCode = (hashCode * 397) ^ Price.GetHashCode();
                return hashCode;
            }
        }

        protected override int GetHashCodeInternal()
        {
            unchecked
            {
                int hashCode = Snack.GetHashCode();
                hashCode = (hashCode ^ 397) ^ Quantity.GetHashCode();
                hashCode = (hashCode ^ 397) ^ Price.GetHashCode();
                return hashCode;
            }
        }

        protected override bool EqualsInternal(SnackPile other)
        {
            return Snack == other.Snack && Quantity == other.Quantity && Price == other.Price;
        }

        public SnackPile SubtractOne()
        {
            return new SnackPile(Snack, Quantity - 1, Price);
        }
    }
}