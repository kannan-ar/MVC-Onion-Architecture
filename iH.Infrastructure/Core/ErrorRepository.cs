namespace iH.Infrastructure.Core
{
    using System;
    using Domain.Core.Repositories;

    public sealed class ErrorRepository : IErrorRepository
    {
        private readonly IHDatabase db;

        public ErrorRepository(IHDatabase db)
        {
            this.db = db;
        }

        public void LogError(DateTime date, string message, string stackTrace)
        {
            db.Execute(@"insert into hr.errors(exception_date, exception_message, exception_stack_trace) 
                values(@exception_date, @exception_message, @exception_stack_trace)",
                new { exception_date = date, exception_message = message, exception_stack_trace = stackTrace });
        }
    }
}
