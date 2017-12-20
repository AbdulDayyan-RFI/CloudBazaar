using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEB.Data
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        object Add(string query, object parameters);
        int Update(string query, object parameters);
        int Delete(string query, object parameters);
        TEntity Get(string query, object parameters);
        IEnumerable<TEntity> GetAll(string query, object parameters);
        Task<IEnumerable<TEntity>> SqlStoredProcedure(string query, object parameters);
        Task<IEnumerable<TEntity>> SqlQuery(string query, object parameters);
        DynamicParameters GetParameter();
        IEnumerable<T> Get<T>(string query, object parameters);
    }
}
