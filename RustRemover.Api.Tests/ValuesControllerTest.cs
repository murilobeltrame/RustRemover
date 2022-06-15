using RustRemover.Api.Controllers;
using System.Linq;
using Xunit;

namespace RustRemover.Api.Tests
{
    public class ValuesControllerTest
    {
        [Fact]
        public void GetShouldReturnAListOfValues()
        {
            var controller = new ValuesController();

            var result = controller.Get();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetWithIdReturnIdAsString()
        {
            var id = int.MaxValue;
            var expectedId = id.ToString();
            var controller = new ValuesController();

            var result = controller.Get(id);

            Assert.Equal(expectedId, result);
        }
    }
}
