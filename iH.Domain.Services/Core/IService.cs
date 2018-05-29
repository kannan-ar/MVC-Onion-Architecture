namespace iH.Domain.Core.Services
{
    using Repositories;

    public interface IService
    {
        IRepository Repository { get; }
    }
}
