using Business.Entities.Concrete;
using Core.DataAccess.Concrete.Dapper;
using Dapper;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Dapper.Repositories
{
    public class AlbumRepository : DpBaseRepository<Album>,IAlbumRepository
    {
        public AlbumRepository(IDbConnection dbConnection,IDbTransaction dbTransaction): base(dbConnection,dbTransaction)
        {

        }
        public async override Task<int> AddAsync(Album album)
        {
            return await Connection.QuerySingleAsync<int>($@"insert into ""Albums"" default values returning ""Id"" " ,transaction: Transaction);
        }
    }
}
