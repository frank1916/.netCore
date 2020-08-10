using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppLibrosNetCORE.Entities
{
    public class EjemploLogica : IEjemploLogica
    {
        public int sumar(int n1, int n2)
        {
            return n1 + n2;
        }
    }
}
