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
        
        public object Add(string query, object parameters)
        {
            using (var conn = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateConnection())
            {
                conn.ConnectionString = connectionString;
                conn.Open();
                return conn.ExecuteScalar(query, parameters, commandType: CommandType.Text);
            }
        }

        public int Update(string query, object parameters)
        {
            using (var conn = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateConnection())
            {
                conn.ConnectionString = connectionString;
                conn.Open();
                return conn.Execute(query, parameters, commandType: CommandType.Text);
            }
        }

        public int Delete(string query, object parameters)
        {
            using (var conn = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateConnection())
            {
                conn.ConnectionString = connectionString;
                conn.Open();
                return conn.Execute(query, parameters, commandType: CommandType.Text);
            }
        }

        public TEntity Get(string query, object parameters)
        {
            using (var conn = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateConnection())
            {
                conn.ConnectionString = connectionString;
                conn.Open();
                return conn.QueryFirstOrDefault<TEntity>(query, parameters, commandType: CommandType.Text);
            }
        }

        public IEnumerable<TEntity> GetAll(string query, object parameters)
        {
            using (var conn = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateConnection())
            {
                conn.ConnectionString = connectionString;
                conn.Open();
                return conn.Query<TEntity>(query, parameters, commandType: CommandType.Text);
            }
        }

        public Task<IEnumerable<TEntity>> SqlStoredProcedure(string query, object parameters)
        {
            using (var conn = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateConnection())
            {
                conn.ConnectionString = connectionString;
                conn.Open();
                return conn.QueryAsync<TEntity>(query, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public Task<IEnumerable<TEntity>> SqlQuery(string query, object parameters)
        {
            using (var conn = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateConnection())
            {
                conn.ConnectionString = connectionString;
                conn.Open();
                return conn.QueryAsync<TEntity>(query, parameters, commandType: CommandType.Text);
            }
        }

        public DynamicParameters GetParameter()
        {
            return new DynamicParameters();
        }

        public IEnumerable<T> Get<T>(string query, object parameters)
        {
            using (var conn = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateConnection())
            {
                conn.ConnectionString = connectionString;
                conn.Open();
                return conn.Query<T>(query, param: parameters, commandType: CommandType.Text);
            }
        }
    }
}
