using Core.DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IPhotoRepository : IRepository<Photo>
    {
        public Task<IEnumerable<Photo>> GetPhotosByUsernameAsync(string username);

        public Task<Photo> GetProfilePhotoByUsernameAsync(string username);

    }
}
