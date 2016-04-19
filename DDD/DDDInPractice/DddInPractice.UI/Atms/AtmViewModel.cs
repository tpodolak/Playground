using DDDInPractice.Logic.Atms;
using DDDInPractice.Logic.SharedKernel;
using DDDInPractice.UI.Common;

namespace DDDInPractice.UI.Atms
{
    public class AtmViewModel : ViewModel
    {
        private readonly PaymentGateway _paymentGateway;
        private readonly AtmRepository _repository;
        private readonly Atm _atm;

        private string _message;
        public string Message
        {
            get { return _message; }
            private set
            {
                _message = value;
                Notify();
            }
        }

        public override string Caption => "ATM";
        public Money MoneyInside => _atm.MoneyInside;
        public string MoneyCharged => _atm.MoneyCharged.ToString("C2");
        public Command<decimal> TakeMoneyCommand { get; private set; }

        public AtmViewModel(Atm atm)
        {
            _atm = atm;
            _repository = new AtmRepository();
            _paymentGateway = new PaymentGateway();

            TakeMoneyCommand = new Command<decimal>(x => x > 0, TakeMoney);
        }

        private void TakeMoney(decimal amount)
        {
            string error = _atm.CanTakeMoney(amount);
            if (error != string.Empty)
            {
                NotifyClient(error);
                return;
            }

            decimal amountWithCommission = _atm.CalculateAmountWithCommission(amount);
            _paymentGateway.ChargePayment(amountWithCommission);
            _atm.TakeMoney(amount);
            _repository.Save(_atm);

            NotifyClient("You have taken " + amount.ToString("C2"));
        }

        private void NotifyClient(string message)
        {
            Message = message;
            Notify(nameof(MoneyInside));
            Notify(nameof(MoneyCharged));
        }
    }
}
