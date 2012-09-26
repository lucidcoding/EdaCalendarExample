using Sales.Domain.Common;
using NHibernate;
using NHibernate.Context;
using NServiceBus;

namespace Sales.MessageHandlers.Core
{
    public class MessageModule : IMessageModule
    {
        private readonly ISessionFactory _sessionFactory;

        public MessageModule(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public void HandleBeginMessage()
        {
            CurrentSessionContext.Bind(_sessionFactory.OpenSession());
        }

        public void HandleEndMessage()
        {
            Cleardown();
        }

        public void HandleError()
        {
            Cleardown();
        }

        private void Cleardown()
        {
            DomainEvents.ClearCallbacks(); 
            _sessionFactory.GetCurrentSession().Dispose();
            CurrentSessionContext.Unbind(_sessionFactory);
        }
    }
}
