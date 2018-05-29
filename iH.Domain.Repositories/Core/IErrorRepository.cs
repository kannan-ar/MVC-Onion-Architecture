namespace iH.Domain.Core.Repositories
{
    using System;

    public interface IErrorRepository
    {
        void LogError(DateTime date, string message, string stackTrace);
    }
}
