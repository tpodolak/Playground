using DddInPractice.Logic;
using DDDInPractice.Logic;
using NHibernate;

namespace DddInPractice.UI.Common
{
    public class MainViewModel : ViewModel
    {
        public MainViewModel()
        {
            SnackMachine snackMachine;
            using (var session = SessionFactory.OpenSession())
            {
                snackMachine = session.Get<SnackMachine>(1);
            }

            var viewModel = new SnackMachineViewModel(snackMachine);
            _dialogService.ShowDialog(viewModel);
        }
    }
}
