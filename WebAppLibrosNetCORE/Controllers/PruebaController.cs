using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppLibrosNetCORE.Entities;
using WebAppLibrosNetCORE.Services;

namespace WebAppLibrosNetCORE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PruebaController : ControllerBase
    {
        public readonly IRepositorioAutores repositorioAutores;

        public PruebaController(IRepositorioAutores repositorioAutores)
        {
            this.repositorioAutores = repositorioAutores;
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<Autor> Get(int id)
        {

            var autor = repositorioAutores.ObtenerPorId(id);

            if (autor == null)
            {
                return NotFound();
            }

            return autor;
        }
    }
}
