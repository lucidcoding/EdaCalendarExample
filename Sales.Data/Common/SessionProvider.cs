using NHibernate;
using NHibernate.Context;

namespace Sales.Data.Common
{
    public class SessionProvider : ISessionProvider
    {
        private readonly ISessionFactory _sessionFactory;

        public SessionProvider(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public ISession GetCurrent()
        {
            if (!CurrentSessionContext.HasBind(_sessionFactory))
            {
                CurrentSessionContext.Bind(_sessionFactory.OpenSession());
            }

            return _sessionFactory.GetCurrentSession();
        }

        public void CloseCurrent()
        {
            ISession currentSession = CurrentSessionContext.Unbind(_sessionFactory);
            currentSession.Close();
            currentSession.Dispose();
        }
    }
}
