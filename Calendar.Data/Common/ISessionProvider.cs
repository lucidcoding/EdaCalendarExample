using NHibernate;

namespace Calendar.Data.Common
{
    public interface ISessionProvider
    {
        ISession GetCurrent();
        void CloseCurrent();
    }
}
