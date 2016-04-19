using DDDInPractice.Logic;
using DDDInPractice.Logic.Atms;
using DDDInPractice.Logic.SnackMachines;
using DDDInPractice.UI.Atms;
using DDDInPractice.UI.SnackMachines;
using NHibernate;

namespace DDDInPractice.UI.Common
{
    public class MainViewModel : ViewModel
    {
        public MainViewModel()
        {
            var atm = new AtmRepository().GetById(3);
            var viewmodel = new AtmViewModel(atm);
            _dialogService.ShowDialog(viewmodel);
            /*
            SnackMachine snackMachine = new SnackMachineRepository().GetById(1);
            var viewModel = new SnackMachineViewModel(snackMachine);
            _dialogService.ShowDialog(viewModel);
            */
        }
    }
}
