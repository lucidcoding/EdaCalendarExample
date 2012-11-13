using NHibernate;

namespace HumanResources.Data.Common
{
    public interface ISessionProvider
    {
        ISession GetCurrent();
        void CloseCurrent();
    }
}
