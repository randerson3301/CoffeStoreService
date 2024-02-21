using MediatR;
using System.Collections.ObjectModel;

namespace CoffeStore.Common.Seedwork
{
    public abstract class Entity
    {
        private ICollection<IDomainEvent> _events;
        public Entity()
        {
            _events = new Collection<IDomainEvent>();
        }

        public IReadOnlyCollection<IDomainEvent> Events => _events.ToList().AsReadOnly();

        public virtual void AddDomainEvent(IDomainEvent @event)
        {
            _events.Add(@event);    
            
        }
    }
}
