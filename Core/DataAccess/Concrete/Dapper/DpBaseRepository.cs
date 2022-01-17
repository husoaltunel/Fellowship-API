using Business.Entities.Abstract;
using Business.Utilities.Sql;
using Core.DataAccess.Abstract;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.Concrete.Dapper
{
    public class DpBaseRepository<TEntity> : BaseConnection, IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        private string entityName;
        public DpBaseRepository(IDbConnection dbConnection, IDbTransaction transaction)
        {
            Connection = dbConnection;
            Transaction = transaction;
            entityName = typeof(TEntity).Name;
        }

        public virtual async Task<int> AddAsync(TEntity entity)
        {
            return await Connection.QuerySingleAsync<int>(SqlQueryUtil<TEntity>.GenerateGenericInsertQuery(entity, entityName), entity, transaction: Transaction);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await Connection.ExecuteAsync($@"delete from ""{entityName}s"" where ""Id"" = {id}", transaction: Transaction);
        }


        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Connection.QueryAsync<TEntity>($@"select * from ""{entityName}s"" ", transaction: Transaction);
        }

        public async Task<IEnumerable<TEntity>> GetByFilterAsync(Func<TEntity, bool> filter)
        {
            return await Task.FromResult(GetAllAsync().Result.Where(filter));
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await Connection.QuerySingleOrDefaultAsync<TEntity>($@"select * from ""{entityName}s"" where ""Id"" = {id}", transaction: Transaction);
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
            return await Connection.ExecuteAsync(SqlQueryUtil<TEntity>.GenerateGenericUpdateQuery(entity, entityName), entity, transaction: Transaction);
        }
    }
}
