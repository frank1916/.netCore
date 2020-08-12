using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppLibrosNetCORE.ModelsDTO
{
    public class AutorDTO: Recurso
    {
        public int id { get; set; }
        [Required]
        public string nombre { get; set; }
        public List<LibroDTO> libros { get; set; }
    }
}
