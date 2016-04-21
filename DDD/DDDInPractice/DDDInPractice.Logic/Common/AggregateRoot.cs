using System.Collections.Generic;

namespace DDDInPractice.Logic.Common
{
    public class AggregateRoot : Entity
    {
        private readonly List<IDomainEvent> domainEvents = new List<IDomainEvent>();
        public virtual IReadOnlyList<IDomainEvent> DomainEvents => domainEvents;

        protected virtual void AddDomainEvent(IDomainEvent domainEvent)
        {
            domainEvents.Add(domainEvent);
        }

        public virtual void ClearEvents()
        {
            domainEvents.Clear();
        }
    }
}