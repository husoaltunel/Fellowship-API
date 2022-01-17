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
    public class PhotoRepository : DpBaseRepository<Photo>, IPhotoRepository
    {
        public PhotoRepository(IDbConnection dbConnection, IDbTransaction transaction) : base(dbConnection, transaction)
        {

        }

        public async Task<IEnumerable<Photo>> GetPhotosByUsernameAsync(string username)
        {

            return await Connection.QueryAsync<Photo>("select * from get_photos_by_username(@username)", new { username = username },transaction: Transaction);
        }
        public async Task<Photo> GetProfilePhotoByUsernameAsync(string username)
        {
            return await Connection.QuerySingleOrDefaultAsync<Photo>("select * from get_profile_photo_by_username(@username)",new {username = username }, transaction: Transaction);
        }
        
    }
}
