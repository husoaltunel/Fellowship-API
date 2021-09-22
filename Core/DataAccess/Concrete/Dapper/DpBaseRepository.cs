using Core.DataAccess.Abstract;
using Core.Entities.Abstract;
using Core.Utilities.Sql;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.Concrete
{
    public class DpBaseRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        private readonly IDbConnection _dbConnection;
        private readonly IDbTransaction _dbTransaction;
        private string entityName;
        public DpBaseRepository(IDbConnection dbConnection, IDbTransaction transaction)
        {
            _dbConnection = dbConnection;
            _dbTransaction = transaction;
            entityName = typeof(TEntity).Name;
        }

        public async Task<int> AddAsync(TEntity entity)
        {
            return await _dbConnection.ExecuteAsync(SqlQueryUtil<TEntity>.GenerateGenericInsertQuery(entity,entityName), entity);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _dbConnection.ExecuteAsync($@"delete from ""{entityName}s"" where ""Id"" = {id}");
        }


        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbConnection.QueryAsync<TEntity>($@"select * from ""{entityName}s"" ");
        }

        public async Task<IEnumerable<TEntity>> GetByFilter(Func<TEntity, bool> filter)
        {
            return await Task.FromResult(GetAllAsync().Result.Where(filter));
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _dbConnection.QuerySingleOrDefaultAsync<TEntity>($@"select * from ""{entityName}s"" where ""Id"" = {id}");
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
           return await _dbConnection.ExecuteAsync(SqlQueryUtil<TEntity>.GenerateGenericUpdateQuery(entity,entityName),entity);
        }
    }
}
