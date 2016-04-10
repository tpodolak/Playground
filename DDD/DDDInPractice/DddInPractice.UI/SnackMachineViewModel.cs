using DddInPractice.Logic;
using DddInPractice.UI.Common;
using DDDInPractice.Logic;
using NHibernate;

namespace DddInPractice.UI
{
    public class SnackMachineViewModel : ViewModel
    {
        private readonly SnackMachine snackMachine;
        public override string Caption => "Snack Machine";

        public SnackMachineViewModel(SnackMachine snackMachine)
        {
            this.snackMachine = snackMachine;
        }
    }
}
