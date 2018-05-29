namespace iH.Domain.Core.Repositories
{
    public interface IRepository
    {
        IHDatabase Db { get; }
        void BeginTransaction();
        void SetConnection(IRepository repository);
        void RollbackTransaction();
        void CommitTransaction();
        void ClearConnection();
    }
}
