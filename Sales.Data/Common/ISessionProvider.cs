using NHibernate;

namespace Sales.Data.Common
{
    public interface ISessionProvider
    {
        ISession GetCurrent();
        void CloseCurrent();
    }
}
