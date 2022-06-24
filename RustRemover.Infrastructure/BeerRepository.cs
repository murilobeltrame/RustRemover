using Ardalis.GuardClauses;
using RustRemover.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RustRemover.Infrastructure
{
    public class BeerRepository : Repository<Beer>
    {

        private const string invalidGuid = "66666666-6666-6666-6666-666666666666";

        public override Task<IEnumerable<Beer>> Fetch(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new Beer[] {
               new Beer{Id=Guid.NewGuid(), Description= "Spaten" },
               new Beer{Id=Guid.NewGuid(), Description= "Skol" },
               new Beer{Id=Guid.NewGuid(), Description= "Stella Artois" },
               new Beer{Id=Guid.NewGuid(), Description= "Budweiser" },
               new Beer{Id=Guid.NewGuid(), Description= "Beck's" },
            }.AsEnumerable());
        }

        public override Task<Beer> Get(Guid id, CancellationToken cancellationToken = default)
        {
            Guard.Against.Default(id, nameof(id));
            Guard.Against.InvalidInput(id, nameof(id), (value) => value != new Guid(invalidGuid));
            return Task.FromResult(new Beer
            {
                Id = id,
                Description = "Brahma"
            });
        }

        public override Task<Beer> Create(Beer data, CancellationToken cancellationToken = default)
        {
            Guard.Against.Null(data, nameof(data));
            return Task.FromResult(new Beer
            {
                Id = data.Id,
                Description = data.Description
            });
        }

        public override async Task Update(Beer data, CancellationToken cancellationToken = default)
        {
            Guard.Against.Null(data, nameof(data));
            Guard.Against.InvalidInput(data.Id, nameof(data), (value) => value != new Guid(invalidGuid));
            await Get(data.Id, cancellationToken);
        }

        public override async Task Delete(Guid id, CancellationToken cancellationToken = default)
        {
            Guard.Against.Default(id, nameof(id));
            Guard.Against.InvalidInput(id, nameof(id), (value) => value != new Guid(invalidGuid));
            await Get(id, cancellationToken);
        }
    }
}
