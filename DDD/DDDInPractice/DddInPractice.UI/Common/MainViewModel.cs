using DDDInPractice.Logic;
using DDDInPractice.Logic.SnackMachines;
using DDDInPractice.UI.SnackMachines;
using NHibernate;

namespace DDDInPractice.UI.Common
{
    public class MainViewModel : ViewModel
    {
        public MainViewModel()
        {
            SnackMachine snackMachine = new SnackMachineRepository().GetById(1);
            var viewModel = new SnackMachineViewModel(snackMachine);
            _dialogService.ShowDialog(viewModel);
        }
    }
}
