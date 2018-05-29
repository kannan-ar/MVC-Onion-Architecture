namespace iH.Domain.Core.Repositories
{
    using System.Data;
    using System.Collections.Generic;

    public interface IHDatabase
    {
        IDbTransaction BeginTransaction();
        IDbConnection Connection { get; }
        void SetConnection(IHDatabase connection);
        T Get<T>(string query, object parameters) where T : class, new();
        IList<T> GetList<T>(string query, object parameters) where T : class, new();
        IList<T> GetList<T>(string query) where T : class, new();
        void Execute(string query, object parameters);
        void Execute(string query, CommandType commandType, object parameters);
        object GetScalar(string query);
        object GetScalar(string query, object parameters);
    }
}
