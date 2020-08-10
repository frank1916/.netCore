using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppLibrosNetCORE.Context;
using WebAppLibrosNetCORE.Entities;

namespace WebAppLibrosNetCORE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LibrosController: ControllerBase
    {
        private readonly AplicationDbContext context;

        public LibrosController(AplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Libro>> Get()
        {
            return context.Libros.Include(x => x.Autor).ToList();
        }

        [HttpGet("{id}", Name = "ObtenerLibro")]
        public ActionResult<Libro> Get(int id)
        {
            var libro = context.Libros.Include(x => x.Autor).FirstOrDefault(x => x.id == id);
            
            if (libro == null)
            {
                return NotFound();
            }
            return libro;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Libro libro)
        {
            context.Libros.Add(libro);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerLibro", new { id = libro.id }, libro);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Libro value)
        {
            if (id != value.id)
            {
                return BadRequest();
            }
            context.Entry(value).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Libro> Delete(int id)
        {
            var libro = context.Libros.FirstOrDefault(x => x.id == id);
            if (libro == null)
            {
                return NotFound();
            }
            context.Libros.Remove(libro);
            context.SaveChanges();
            return libro;
        }
    }
}
