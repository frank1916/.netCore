using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppLibrosNetCORE.Context;
using WebAppLibrosNetCORE.Entities;
using WebAppLibrosNetCORE.Helpers;
using WebAppLibrosNetCORE.ModelsDTO;

namespace WebAppLibrosNetCORE.Controllers
{

    //Convencion para simplificar la codificacion
    [ApiController]
    [Route("[controller]")]
    public class AutoresController : ControllerBase
    {
        private readonly AplicationDbContext context;
        private readonly ILogger<AutoresController> logger;
        private readonly IMapper mapper;
        private readonly IEjemploLogica ejemploLogica;

        public AutoresController(AplicationDbContext context, ILogger<AutoresController> logger, IMapper mapper,
            IEjemploLogica ejemploLogica)
        {
            this.context = context;
            this.logger = logger;
            this.mapper = mapper;
            this.ejemploLogica = ejemploLogica;
        }

        [HttpGet("listado")]
        //filltro personalizado
        [ServiceFilter(typeof(MiFiltroDeAccion))]
        public async Task<ActionResult<IEnumerable<AutorDTO>>> Get()
        {
            //throw new NotImplementedException();
            this.logger.LogInformation("obteniendo los autores" + this.ejemploLogica.sumar(3, 4));
            var autores = await context.Autores.Include(x => x.libros).ToListAsync();
            var autoresDTO = this.mapper.Map<List<AutorDTO>>(autores);

            return autoresDTO;
        }

        [HttpGet("{id}", Name = "ObtenerAutor")]
        public async Task<ActionResult<AutorDTO>> Get(int id)
        {
            var autor = await context.Autores.Include(x => x.libros).FirstOrDefaultAsync(x => x.id == id);
            if (autor == null)
            {
                this.logger.LogWarning($"el autor de id {id} no ha sido encontrado");
                return NotFound();
            }

            var autorDTO = this.mapper.Map<AutorDTO>(autor);

            return autorDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AutorCreacionDTO autorCreacion)
        {
            var autor = this.mapper.Map<Autor>(autorCreacion);
            context.Autores.Add(autor);
            await context.SaveChangesAsync();
            var autorDTO = mapper.Map<AutorDTO>(autor);
            return new CreatedAtRouteResult("ObtenerAutor", new { id = autor.id }, autorDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] AutorCreacionDTO value)
        {
            var autor = this.mapper.Map<Autor>(value);
            autor.id = id;
            context.Entry(autor).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Autor>> Delete(int id)
        {
            //var autor = await context.Autores.FirstOrDefaultAsync(x => x.id == id);
            //optimizar linea de arriba
            var autorId = await context.Autores.Select(x => x.id).FirstOrDefaultAsync(x => x == id);
           
            if (autorId == default(int))
            {
                return NotFound();
            }
            context.Autores.Remove(new Autor { id = autorId});
            await context.SaveChangesAsync();
            return NoContent();
        }


        [HttpPatch("{id}")]
        public async Task<ActionResult> patch(int id, [FromBody] JsonPatchDocument<AutorCreacionDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var autorBD = await this.context.Autores.FirstOrDefaultAsync(x => x.id == id);

            if (autorBD == null)
            {
                return NotFound();
            }

            var autorDTO  = this.mapper.Map<AutorCreacionDTO>(autorBD);

            patchDocument.ApplyTo(autorDTO, ModelState);

            this.mapper.Map(autorDTO, autorBD);

            var isVaild = TryValidateModel(autorBD);

            if (!isVaild)
            {
                return BadRequest(ModelState);
            }

            await context.SaveChangesAsync();
            return NoContent();

            //EXAMPLE JSON
            /**
             * [{
                "op": "replace",
                "path": "/nombre",
                "value": "Juan15"
                }]
             */

        }

    }
}
