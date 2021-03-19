using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.Api.Author.Modelo;

namespace TiendaServicios.Api.Author.Persistencia
{
    public class ContextoAutor : DbContext
    {
        public ContextoAutor(DbContextOptions<ContextoAutor> options): base(options) { }

        public DbSet<AutorLibro>  AutorLibro { get; set; }


    }
}
