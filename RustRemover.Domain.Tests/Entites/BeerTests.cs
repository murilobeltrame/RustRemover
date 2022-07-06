using RustRemover.Domain.Entities;
using Xunit;

namespace RustRemover.Domain.Tests.Entites
{
    public class BeerTests
    {
        [Fact]
        public void ShouldBeInstantiated()
        {
            var beer = new Beer();

            Assert.NotNull(beer);
        }
    }
}
