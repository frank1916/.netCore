using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppLibrosNetCORE.ModelsDTO
{
    public class AutorCreacionDTO
    {
        [Required]
        public string nombre { get; set; }
    }
}
