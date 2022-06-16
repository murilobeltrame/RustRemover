using RustRemover.Api.Controllers;
using RustRemover.Api.Models;
using System;
using System.Net;
using System.Web.Http.Results;
using Xunit;

namespace RustRemover.Api.Tests
{
    public class BeerControllerTest
    {
        private const string invalidGuid = "66666666-6666-6666-6666-666666666666";

        [Fact]
        public void GetShouldReturnAList()
        {
            var controller = new BeersController();
            var actionResult = controller.Get();
            var contentResult = actionResult as OkNegotiatedContentResult<Beer[]>;

            Assert.IsType<OkNegotiatedContentResult<Beer[]>>(actionResult);
            Assert.NotNull(contentResult);
            Assert.NotNull(contentResult.Content);
            Assert.Equal(5, contentResult.Content.Length);
        }

        [Fact]
        public void GetWithValidIdShouldReturnBrahmaWithThatId()
        {
            var id = Guid.NewGuid();
            var controller = new BeersController();

            var actionResult = controller.Get(id);
            var contentResult = actionResult as OkNegotiatedContentResult<Beer>;

            Assert.IsType<OkNegotiatedContentResult<Beer>>(actionResult);
            Assert.NotNull(contentResult);
            Assert.NotNull(contentResult.Content);
            Assert.Equal(id, contentResult.Content.Id);
            Assert.Equal("Brahma", contentResult.Content.Description);
        }

        [Fact]
        public void GetWithEmptyIdShouldReturnBadRequest()
        {
            var controller = new BeersController();

            var actionResult = controller.Get(Guid.Empty);

            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public void GetWithInvalidIdShouldReturnNotFound()
        {
            var controller = new BeersController();

            var actionResult = controller.Get(new Guid(invalidGuid));

            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public void PostWithValidBeerShouldReturnCreatedWithThatBeerAndLink()
        {
            var id = Guid.NewGuid();
            var description = "Original";
            var beer = new Beer { Id = id, Description = description };
            var controller = new BeersController();

            var actionResult = controller.Post(beer);
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
        public void PostWithNullDataShouldReturnBadRequest()
        {
            var controller = new BeersController();

            var actionResult = controller.Post(null);

            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public void PutWithValidBeerShouldReturnNoContent()
        {
            var id = Guid.NewGuid();
            var description = "Original";
            var beer = new Beer { Id = id, Description = description };
            var controller = new BeersController();

            var actionResult = controller.Put(id, beer);
            var contentResult = actionResult as StatusCodeResult;

            Assert.IsType<StatusCodeResult>(actionResult);
            Assert.NotNull(contentResult);
            Assert.Equal(HttpStatusCode.NoContent, contentResult.StatusCode);
        }

        [Fact]
        public void PutWithNullDataShouldReturnBadRequest()
        {
            var id = Guid.NewGuid();
            var controller = new BeersController();

            var actionResult = controller.Put(id, null);

            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public void PutWithEmptyIdShouldReturnBadRequest()
        {
            var controller = new BeersController();

            var actionResult = controller.Put(Guid.Empty, new Beer());

            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public void PutWithInvalidReferenceShouldReturnBadRequest()
        {
            var id = Guid.NewGuid();
            var beer = new Beer { Id = Guid.NewGuid() };
            var controller = new BeersController();

            var actionResult = controller.Put(id, beer);

            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public void PutWithInvalidIdShouldReturnNotFound()
        {
            var id = new Guid(invalidGuid);
            var beer = new Beer { Id = id };
            var controller = new BeersController();

            var actionResult = controller.Put(id, beer);

            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public void DeleteWithValidIdShouldReturnNoContent()
        {
            var id = Guid.NewGuid();
            var controller = new BeersController();

            var actionResult = controller.Delete(id);
            var contentResult = actionResult as StatusCodeResult;

            Assert.IsType<StatusCodeResult>(actionResult);
            Assert.NotNull(contentResult);
            Assert.Equal(HttpStatusCode.NoContent, contentResult.StatusCode);
        }

        [Fact]
        public void DeleteWithEmptyIdShouldReturnBadRequest()
        {
            var controller = new BeersController();

            var actionResult = controller.Delete(Guid.Empty);

            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public void DeleteWithInvalidIdShouldReturnNotFound()
        {
            var controller = new BeersController();

            var actionResult = controller.Delete(new Guid(invalidGuid));

            Assert.IsType<NotFoundResult>(actionResult);
        }
    }
}
