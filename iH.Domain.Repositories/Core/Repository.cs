namespace iH.Domain.Core.Repositories
{
    using System.Data;

    public abstract class Repository : IRepository
    {
        protected IHDatabase db;
        protected IDbTransaction transaction;
        
        public IHDatabase Db
        {
            get
            {
                return db;
            }
        }

        public Repository(IHDatabase db)
        {
            this.db = db;
        }

        public void BeginTransaction()
        {
            if (transaction == null)
            {
                transaction = db.BeginTransaction();
            }
        }

        public void SetConnection(IRepository repository)
        {
            db.SetConnection(repository.Db);
        }

        public void RollbackTransaction()
        {
            if (transaction != null)
            {
                transaction.Rollback();
                ClearConnection();
            }
        }

        public void CommitTransaction()
        {
            if (transaction != null)
            {
                transaction.Commit();
                ClearConnection();
            }
        }

        public void ClearConnection()
        {
            transaction = null;
            db.SetConnection(null);
        }
    }
}
