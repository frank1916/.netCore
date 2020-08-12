using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppLibrosNetCORE.ModelsDTO
{
    public class Enlace
    {

        public Enlace(string href, string rel, string metodo)
        {
            Href = href;
            Rel = rel;
            Metodo = metodo;
        }

        public string Href { get; }
        public string Rel { get; }
        public string Metodo { get; }
    }
}
