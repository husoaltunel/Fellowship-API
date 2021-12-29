using Business.Concrete;
using Core.Utilities.Results.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Managers.Photo.Commands.InsertPhoto
{
    public class Handler :BaseConnection, IRequestHandler<InsertUserPhotosCommand, IDataResult<int>>
    {
        public Task<IDataResult<int>> Handle(InsertUserPhotosCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
