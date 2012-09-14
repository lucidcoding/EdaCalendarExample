using Calendar.Domain.Common;
using NHibernate;
using NHibernate.Context;
using NServiceBus;

namespace Calendar.MessageHandlers.Core
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
            DomainEvents.ClearCallbacks();    
        }

        public void HandleError()
        {
        }
    }
}
