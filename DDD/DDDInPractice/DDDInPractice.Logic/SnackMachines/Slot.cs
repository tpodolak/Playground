using DDDInPractice.Logic.Common;

namespace DDDInPractice.Logic.SnackMachines
{
    public class Slot : AggregateRoot
    {
        public virtual SnackPile SnackPile { get; set; }
        public virtual SnackMachine SnackMachine { get; set; }
        public virtual int Position { get; set; }

        public Slot()
        {

        }

        public Slot(SnackMachine snackMachine, int position)
        {
            SnackMachine = snackMachine;
            Position = position;
            SnackPile = new SnackPile(null, 0, 0);
        }
    }
}