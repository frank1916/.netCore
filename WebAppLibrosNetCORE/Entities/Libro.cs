using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppLibrosNetCORE.Entities
{
    public class Libro
    {
        public int id { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string titulo { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public int autorId { get; set; }
        public Autor Autor { get; set; }
    }
}
