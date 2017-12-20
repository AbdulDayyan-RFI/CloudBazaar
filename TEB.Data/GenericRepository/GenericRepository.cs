using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace TEB.Data
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
        public IDbConnection GetConnection
        {
            get
            {
                var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
                var conn = factory.CreateConnection();
                conn.ConnectionString = connectionString;
                conn.Open();
                return conn;
            }
        }

        public object Add(string query, object parameters)
        {
            return SqlMapper.ExecuteScalar(GetConnection, query, parameters, commandType: CommandType.Text);
        }

        public int Update(string query, object parameters)
        {
            return SqlMapper.Execute(GetConnection, query, parameters, commandType: CommandType.Text);
        }

        public int Delete(string query, object parameters)
        {
            return SqlMapper.Execute(GetConnection, query, parameters, commandType: CommandType.Text);
        }

        public TEntity Get(string query, object parameters)
        {
            return SqlMapper.QueryFirstOrDefault<TEntity>(GetConnection, query, parameters, commandType: CommandType.Text);
        }

        public IEnumerable<TEntity> GetAll(string query, object parameters)
        {
            return SqlMapper.Query<TEntity>(GetConnection, query, parameters, commandType: CommandType.Text);
        }

        public Task<IEnumerable<TEntity>> SqlStoredProcedure(string query, object parameters)
        {
            return SqlMapper.QueryAsync<TEntity>(GetConnection, query, parameters, commandType: CommandType.StoredProcedure);
        }

        public Task<IEnumerable<TEntity>> SqlQuery(string query, object parameters)
        {
            return SqlMapper.QueryAsync<TEntity>(GetConnection, query, parameters, commandType: CommandType.Text);
        }

        public DynamicParameters GetParameter()
        {
            return new DynamicParameters();
        }

        public IEnumerable<T> Get<T>(string query, object parameters)
        {
            return SqlMapper.Query<T>(GetConnection, query, param: parameters, commandType: CommandType.Text);
        }
    }
}
