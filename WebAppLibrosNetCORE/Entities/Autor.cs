using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAppLibrosNetCORE.Helpers;

namespace WebAppLibrosNetCORE.Entities
{
    public class Autor: IValidatableObject
    {
        public int id { get; set; }
        //[PrimeraLetraMayuscula]
        [StringLength(10, ErrorMessage ="el campo debe tener {1} caracteres o menos")]
        public string nombre { get; set; }
        public List<Libro> libros { get; set; }

        /**
         * Validacion por modelo
         */
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(nombre))
            {
                var primeraLetra = nombre[0].ToString();
                if (primeraLetra != primeraLetra.ToUpper())
                {
                    yield return new ValidationResult("La primera letra debe ser mayuscula (valid por modelo)", new string[] {nameof(nombre)});
                }
            }
        }
    }
}
