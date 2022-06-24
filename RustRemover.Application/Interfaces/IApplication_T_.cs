using RustRemover.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RustRemover.Application.Interfaces
{
    public interface IApplication<T> where T : IEntity
    {
        Task<IEnumerable<T>> Fetch(CancellationToken cancellationToken = default);

        Task<T> Get(Guid id, CancellationToken cancellationToken = default);

        Task<T> Create(T data, CancellationToken cancellationToken = default);

        Task Update(T data, CancellationToken cancellationToken = default);

        Task Delete(Guid id, CancellationToken cancellationToken = default);
    }
}
