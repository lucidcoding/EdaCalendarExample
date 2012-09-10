namespace HumanResources.Domain.Common
{
    public abstract class DomainEvent<T> 
    {
        private readonly T _source;

        protected DomainEvent(T source)
        {
            _source = source;
        }

        public T Source
        {
            get { return _source; }
        }
    }
}
