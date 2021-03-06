using Core.DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IAlbumRepository : IRepository<Album>
    {
        public new Task<int> InsertAsync(Album album);
    }
}
