using RustRemover.Api.Controllers;
using RustRemover.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Xunit;

namespace RustRemover.Api.Tests.Unit
{
    public class BeerControllerTests
    {
        private const string invalidGuid = "66666666-6666-6666-6666-666666666666";

        [Fact]
        public async Task GetShouldReturnAList()
        {
            var controller = new BeersController();
            var actionResult = await controller.Get();
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<Beer>>;

            Assert.IsType<OkNegotiatedContentResult<IEnumerable<Beer>>>(actionResult);
            Assert.NotNull(contentResult);
            Assert.NotNull(contentResult.Content);
            Assert.Equal(5, contentResult.Content.Count());
        }

        [Fact]
        public async Task GetWithValidIdShouldReturnBrahmaWithThatId()
        {
            var id = Guid.NewGuid();
            var controller = new BeersController();

            var actionResult = await controller.Get(id);
            var contentResult = actionResult as OkNegotiatedContentResult<Beer>;

            Assert.IsType<OkNegotiatedContentResult<Beer>>(actionResult);
            Assert.NotNull(contentResult);
            Assert.NotNull(contentResult.Content);
            Assert.Equal(id, contentResult.Content.Id);
            Assert.Equal("Brahma", contentResult.Content.Description);
        }

        [Fact]
        public async Task GetWithEmptyIdShouldReturnBadRequest()
        {
            var controller = new BeersController();

            var actionResult = await controller.Get(Guid.Empty);

            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async Task GetWithInvalidIdShouldReturnNotFound()
        {
            var controller = new BeersController();

            var actionResult = await controller.Get(new Guid(invalidGuid));

            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public async Task PostWithValidBeerShouldReturnCreatedWithThatBeerAndLink()
        {
            var id = Guid.NewGuid();
            var description = "Original";
            var beer = new Beer { Id = id, Description = description };
            var controller = new BeersController();

            var actionResult = await controller.Post(beer);
            var contentResult = actionResult as CreatedAtRouteNegotiatedContentResult<Beer>;

            Assert.IsType<CreatedAtRouteNegotiatedContentResult<Beer>>(actionResult);
            Assert.NotNull(contentResult);
            Assert.NotNull(contentResult.Content);
            Assert.Equal(id, contentResult.Content.Id);
            Assert.Equal(description, contentResult.Content.Description);
            Assert.Equal(nameof(controller.Get), contentResult.RouteName);
            Assert.Equal(id, contentResult.RouteValues["Id"]);
        }

        [Fact]
        public async Task PostWithNullDataShouldReturnBadRequest()
        {
            var controller = new BeersController();

            var actionResult = await controller.Post(null);

            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async Task PutWithValidBeerShouldReturnNoContent()
        {
            var id = Guid.NewGuid();
            var description = "Original";
            var beer = new Beer { Id = id, Description = description };
            var controller = new BeersController();

            var actionResult = await controller.Put(id, beer);
            var contentResult = actionResult as StatusCodeResult;

            Assert.IsType<StatusCodeResult>(actionResult);
            Assert.NotNull(contentResult);
            Assert.Equal(HttpStatusCode.NoContent, contentResult.StatusCode);
        }

        [Fact]
        public async Task PutWithNullDataShouldReturnBadRequest()
        {
            var id = Guid.NewGuid();
            var controller = new BeersController();

            var actionResult = await controller.Put(id, null);

            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async Task PutWithEmptyIdShouldReturnBadRequest()
        {
            var controller = new BeersController();

            var actionResult = await controller.Put(Guid.Empty, new Beer());

            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async Task PutWithInvalidReferenceShouldReturnBadRequest()
        {
            var id = Guid.NewGuid();
            var beer = new Beer { Id = Guid.NewGuid() };
            var controller = new BeersController();

            var actionResult = await controller.Put(id, beer);

            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async Task PutWithInvalidIdShouldReturnNotFound()
        {
            var id = new Guid(invalidGuid);
            var beer = new Beer { Id = id };
            var controller = new BeersController();

            var actionResult = await controller.Put(id, beer);

            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public async Task DeleteWithValidIdShouldReturnNoContent()
        {
            var id = Guid.NewGuid();
            var controller = new BeersController();

            var actionResult = await controller.Delete(id);
            var contentResult = actionResult as StatusCodeResult;

            Assert.IsType<StatusCodeResult>(actionResult);
            Assert.NotNull(contentResult);
            Assert.Equal(HttpStatusCode.NoContent, contentResult.StatusCode);
        }

        [Fact]
        public async Task DeleteWithEmptyIdShouldReturnBadRequest()
        {
            var controller = new BeersController();

            var actionResult = await controller.Delete(Guid.Empty);

            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async Task DeleteWithInvalidIdShouldReturnNotFound()
        {
            var controller = new BeersController();

            var actionResult = await controller.Delete(new Guid(invalidGuid));

            Assert.IsType<NotFoundResult>(actionResult);
        }
    }
}
