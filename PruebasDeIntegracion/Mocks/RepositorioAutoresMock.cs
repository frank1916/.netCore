using System;
using System.Collections.Generic;
using System.Text;
using WebAppLibrosNetCORE.Entities;
using WebAppLibrosNetCORE.Services;

namespace PruebasDeIntegracion.Mocks
{
    class RepositorioAutoresMock : IRepositorioAutores
    {
        public Autor ObtenerPorId(int id)
        {
            if (id == 0)
            {
                return null;
            }
            return new Autor()
            {
                id = id,
                nombre = "Pepito pruebas",
            };
        }
    }
}
