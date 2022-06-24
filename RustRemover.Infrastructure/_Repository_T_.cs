using RustRemover.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RustRemover.Infrastructure
{
    public abstract class Repository<T> : IRepository<T> where T : IEntity
    {
        public virtual Task<T> Create(T data, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task Delete(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IEnumerable<T>> Fetch(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<T> Get(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task Update(T data, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
