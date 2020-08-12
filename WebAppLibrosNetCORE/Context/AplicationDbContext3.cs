using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppLibrosNetCORE.Entities;

namespace WebAppLibrosNetCORE.Context
{
    public class AplicationDbContext: DbContext
    {
        public AplicationDbContext(DbContextOptions <AplicationDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Autor> Autores { get; set; }
        public DbSet<Libro> Libros { get; set; }
    }
}
