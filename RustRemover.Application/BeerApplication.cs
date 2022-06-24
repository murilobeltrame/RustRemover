using RustRemover.Application.Interfaces;
using RustRemover.Domain.Entities;
using RustRemover.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RustRemover.Application
{
    public class BeerApplication : IApplication<Beer>
    {
        private readonly IRepository<Beer> _repository;

        public BeerApplication(IRepository<Beer> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Beer>> Fetch(CancellationToken cancellationToken = default)
        {
            return await _repository.Fetch(cancellationToken);
        }

        public async Task<Beer> Get(Guid id, CancellationToken cancellationToken = default)
        {
            return await _repository.Get(id, cancellationToken);
        }

        public async Task<Beer> Create(Beer beer, CancellationToken cancellationToken = default)
        {
            return await _repository.Create(beer, cancellationToken);
        }

        public async Task Update(Beer beer, CancellationToken cancellationToken = default)
        {
            await _repository.Update(beer, cancellationToken);
        }

        public async Task Delete(Guid id, CancellationToken cancellationToken = default)
        {
            await _repository.Delete(id, cancellationToken);
        }
    }
}
