using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppLibrosNetCORE.ModelsDTO;

namespace WebAppLibrosNetCORE.Controllers
{
    [ApiController]
    [Route("api")]
    public class RootController: ControllerBase
    {
        public RootController()
        {

        }

        [HttpGet (Name = "GetRoot")]
        public ActionResult <IEnumerable<Enlace>> Get ()
        {
            List<Enlace> enlaces = new List<Enlace>();

            enlaces.Add(new Enlace(href: Url.Link("GetRoot", new { }), rel: "self", metodo: "GET"));
            enlaces.Add(new Enlace(href: Url.Link("listado", new { }), rel: "autores", metodo: "GET"));

            return enlaces;
        }


    }
}
