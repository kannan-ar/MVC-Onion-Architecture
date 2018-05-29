namespace iH.Domain.Core.Services
{
    using System;

    public interface IErrorLogService
    {
        void LogError(DateTime date, string message, string stackTrace);
    }
}
