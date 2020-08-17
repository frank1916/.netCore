using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppLibrosNetCORE.Entities;

namespace WebAppLibrosNetCORE.Services
{
    public interface IRepositorioAutores
    {
        Autor ObtenerPorId(int id);
    }
}
