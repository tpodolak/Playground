using DDDInPractice.Logic.Common;

namespace DDDInPractice.Logic.Atms
{
    public class BalanceChangedEvent : IDomainEvent
    {
        public decimal Delta { get; private set; }

        public BalanceChangedEvent(decimal delta)
        {
            Delta = delta;
        }
    }
}