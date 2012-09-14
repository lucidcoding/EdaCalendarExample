namespace Calendar.Domain.Common
{
    public interface IDomainEventHandler<T> 
    {
        void Handle(T @event);
    }
}
