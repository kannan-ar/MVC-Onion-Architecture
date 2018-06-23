namespace iH.Infrastructure.Core
{
    using System.Data;
    using System.Collections.Generic;
    using Npgsql;
    using MariGold.Data;

    using Repo = Domain.Core.Repositories;

    public class Database : Repo.IHDatabase
    {
        private readonly string connectionString;
        protected IDbConnection connection;

        private IDbConnection GetConnection()
        {
            if (connection == null || (connection != null && connection.State != ConnectionState.Open))
            {
                connection = new NpgsqlConnection(connectionString);
                connection.Open();
            }

            return connection;
        }

        public IDbConnection Connection
        {
            get
            {
                var conn = new NpgsqlConnection(connectionString);

                conn.Open();
                return conn;
            }
        }

        public Database(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IDbTransaction BeginTransaction()
        {
            GetConnection();
            return connection.BeginTransaction();
        }

        public void SetConnection(Repo.IHDatabase db)
        {
            Database datbase = db as Database;

            if (datbase != null)
            {
                this.connection = datbase.connection;
            }
        }

        public T Get<T>(string query, object parameters)
            where T : class, new()
        {
            IDbConnection conn = GetConnection();

            Config.UnderscoreToPascalCase = true;

            return conn.Get<T>(query, parameters);
        }

        public IList<T> GetList<T>(string query, object parameters)
            where T : class, new()
        {
            IDbConnection conn = GetConnection();

            Config.UnderscoreToPascalCase = true;

            return conn.GetList<T>(query, parameters);
        }

        public IList<T> GetList<T>(string query)
            where T : class, new()
        {
            IDbConnection conn = GetConnection();

            Config.UnderscoreToPascalCase = true;

            return conn.GetList<T>(query);
        }

        public void Execute(string query, object parameters)
        {
            IDbConnection conn = GetConnection();
            conn.Execute(query, parameters);
        }

        public void Execute(string query, CommandType commandType, object parameters)
        {
            IDbConnection conn = GetConnection();

            conn.Execute(query, commandType, parameters);
        }

        public object GetScalar(string query)
        {
            IDbConnection conn = GetConnection();

            return conn.GetScalar(query);
        }

        public object GetScalar(string query, object parameters)
        {
            IDbConnection conn = GetConnection();

            return conn.GetScalar(query, parameters);
        }
    }
}
