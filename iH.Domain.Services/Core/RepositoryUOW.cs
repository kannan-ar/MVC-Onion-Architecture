namespace iH.Domain.Core.Services
{
    using System.Collections.Generic;

    public sealed class RepositoryUOW
    {
        private List<IService> services;

        private void ClearConnection()
        {
            for (int i = 1; services.Count > i; i++)
            {
                services[i].Repository.ClearConnection();
            }
        }

        public RepositoryUOW()
        {
            services = new List<IService>();
        }

        public void AddService(IService service)
        {
            services.Add(service);
        }

        public void Begin()
        {
            if (services.Count > 0)
            {
                IService service = services[0];

                service.Repository.BeginTransaction();

                for (int i = 1; services.Count > i; i++)
                {
                    services[i].Repository.SetConnection(service.Repository);
                }
            }
        }

        public void Rollback()
        {
            if (services.Count > 0)
            {
                IService service = services[0];

                service.Repository.RollbackTransaction();

                ClearConnection();
            }
        }

        public void Commit()
        {
            if (services.Count > 0)
            {
                IService service = services[0];

                service.Repository.CommitTransaction();

                ClearConnection();
            }
        }
    }
}
