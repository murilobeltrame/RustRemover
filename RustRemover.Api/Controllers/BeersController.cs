using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Web.Http;

namespace RustRemover.Api.Controllers
{
    public class BeersController : ApiController
    {
        private const string invalidGuid = "66666666-6666-6666-6666-666666666666";

        public IHttpActionResult Get()
        {
            return Ok(new Beer[] { 
               new Beer{Id=Guid.NewGuid(), Description= "Spaten" },
               new Beer{Id=Guid.NewGuid(), Description= "Skol" },
               new Beer{Id=Guid.NewGuid(), Description= "Stella Artois" },
               new Beer{Id=Guid.NewGuid(), Description= "Budweiser" },
               new Beer{Id=Guid.NewGuid(), Description= "Beck's" },
            });
        }

        public IHttpActionResult Get(Guid id)
        {
            if (id == Guid.Empty) return BadRequest();
            if (id == new Guid(invalidGuid)) return NotFound();
            return Ok(new Beer
            {
                Id = id,
                Description = "Brahma"
            });
        }

        public IHttpActionResult Post(Beer beer)
        {
            if (beer == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();
            return CreatedAtRoute(nameof(Get), new { Id = beer.Id }, beer);
        }

        public IHttpActionResult Put(Guid id, Beer beer)
        {
            if (id == Guid.Empty) return BadRequest();
            if (beer == null) return BadRequest();
            if (beer.Id != id) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();
            if (id == new Guid(invalidGuid)) return NotFound();

            return StatusCode(HttpStatusCode.NoContent);
        }

        public IHttpActionResult Delete(Guid id)
        {
            if (id == Guid.Empty) return BadRequest();
            if (id == new Guid(invalidGuid)) return NotFound();

            return StatusCode(HttpStatusCode.NoContent);
        }
    }

    public class Beer
    {
        public Guid Id { get; set; }
        
        [Required]
        public string Description { get; set; }
    }
}
