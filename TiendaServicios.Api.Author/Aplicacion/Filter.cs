using AutoMapper;
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
    public class Filter
    {
        public class filter : IRequest<AutorDto>
        {
            public string AutorGuid { get; set; }
        }

        public class HandlerFilter : IRequestHandler<filter, AutorDto>
        {
            private readonly ContextoAutor _context;

            private readonly IMapper _mapper;
            public HandlerFilter(ContextoAutor context,IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<AutorDto> Handle(filter request, CancellationToken cancellationToken)
            {
                var autor= await _context.AutorLibro.Where(autor => autor.AutorLibroGuid == request.AutorGuid).FirstOrDefaultAsync();

                if(autor == null)
                {
                    throw new Exception("No se encontro el Autor");
                }
                var autorDto = _mapper.Map<AutorLibro, AutorDto>(autor);
                return autorDto;
            }
        }


    }
}
