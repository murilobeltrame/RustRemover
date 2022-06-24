using RustRemover.Domain.Interfaces;
using System;

namespace RustRemover.Domain.Entities
{
    public class Beer: IEntity
    {
        public Guid Id { get; set; }

        public string Description { get; set; }
    }
}
