using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.Modelo;
using TiendaServicios.Api.Libro.Persistencia;

namespace TiendaServicios.Api.Libro.Aplicacion
{
    public class ConsultaFilter
    {
        public class LibroUnico : IRequest<LibroMaterialDto>
        {
            public Guid? LibroId { get; set; }
        }

        public class Handler : IRequestHandler<LibroUnico, LibroMaterialDto>
        {
            private readonly ContextoLibreria _context;
            private readonly IMapper _mapper;

            public Handler(ContextoLibreria contexto,IMapper mapper)
            {
                _context = contexto;
                _mapper = mapper;
            }
           
            public async Task<LibroMaterialDto> Handle(LibroUnico request, CancellationToken cancellationToken)
            {
                var libro = await _context.LibreriaMaterial.Where(book => book.LibreriaMaterialId == request.LibroId).FirstOrDefaultAsync();

                if(libro == null)
                {
                    throw new Exception("No se encontraron resultados");
                }

                var libroDto = _mapper.Map<LibreriaMaterial, LibroMaterialDto>(libro);

                return libroDto;
            }
        }
    }
}
