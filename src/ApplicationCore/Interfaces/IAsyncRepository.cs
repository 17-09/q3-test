using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IAsyncRepository<T> where T : IEntity
    {
        public Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken);
        public Task<T> Get(Guid id, CancellationToken cancellationToken);
        public Task Update(T entity, CancellationToken cancellationToken);

        // Other generic methods
    }
}