using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Author.Modelo;
using TiendaServicios.Api.Author.Persistencia;

namespace TiendaServicios.Api.Author.Aplicacion
{
    public class Consulta
    {
        public class ListaAutor : IRequest <List<AutorLibro>>
        {

        }

        public class Manejador : IRequestHandler<ListaAutor, List<AutorLibro>>
        {
            private readonly ContextoAutor _contexto;

            public Manejador(ContextoAutor context)
            {
                _contexto = context;
            }

            public async Task<List<AutorLibro>> Handle(ListaAutor request, CancellationToken cancellationToken)
            {
               var autores = await _contexto.AutorLibro.ToListAsync();
                return autores;
            }
        }

    }
}
