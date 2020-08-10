using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppLibrosNetCORE.ModelsDTO
{
    public class LibroDTO
    {
        public int id { get; set; }
        [Required]
        public string titulo { get; set; }
        public int autorId { get; set; }
    }
}
