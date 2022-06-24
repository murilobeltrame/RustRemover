using RustRemover.Application;
using RustRemover.Application.Interfaces;
using RustRemover.Domain.Entities;
using RustRemover.Infrastructure;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace RustRemover.Api.Controllers
{
    public class BeersController : ApiController
    {
        private readonly IApplication<Beer> _application;

        public BeersController() : this(new BeerApplication(new BeerRepository())) { }

        public BeersController(IApplication<Beer> application)
        {
            _application = application;
        }

        public async Task<IHttpActionResult> Get()
        {
            return Ok(await _application.Fetch());
        }

        public async Task<IHttpActionResult> Get(Guid id)
        {
            if (id == Guid.Empty) return BadRequest();
            try
            {
                return Ok(await _application.Get(id));
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        public async Task<IHttpActionResult> Post(Beer beer)
        {
            if (beer == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();
            var createdBeer = await _application.Create(beer);
            return CreatedAtRoute(nameof(Get), new { createdBeer.Id }, createdBeer);
        }

        public async Task<IHttpActionResult> Put(Guid id, Beer beer)
        {
            if (id == Guid.Empty) return BadRequest();
            if (beer == null) return BadRequest();
            if (beer.Id != id) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                await _application.Update(beer);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        public async Task<IHttpActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty) return BadRequest();

            try
            {
                await _application.Delete(id);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }
    }
}
