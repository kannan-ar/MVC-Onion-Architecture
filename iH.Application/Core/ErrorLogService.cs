namespace iH.Application.Core
{
    using System;
    using Domain.Core.Repositories;
    using Domain.Core.Services;

    public class ErrorLogService : IErrorLogService
    {
        private readonly IErrorRepository repository;

        public ErrorLogService(IErrorRepository repository)
        {
            this.repository = repository;
        }

        public void LogError(DateTime date, string message, string stackTrace)
        {
            repository.LogError(date, message, stackTrace);
        }
    }
}
