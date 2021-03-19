using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Author.Modelo;
using TiendaServicios.Api.Author.Persistencia;

namespace TiendaServicios.Api.Author.Aplicacion
{
    public class Nuevo
    {


        public class Ejecuta : IRequest
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public DateTime? FehcaNacimiento { get; set; }
        }


        public class Manejador : IRequestHandler<Ejecuta>
        {
            public readonly ContextoAutor _contexto;

            public Manejador(ContextoAutor contexto)
            {
                this._contexto = contexto;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var autorLibro = new AutorLibro
                {
                    Nombre = request.Nombre,
                    Apellido = request.Apellido,
                    FechaNacimiento = request.FehcaNacimiento,
                    AutorLibroGuid = Convert.ToString(Guid.NewGuid())
                };
                _contexto.Add(autorLibro);

                if(await _contexto.SaveChangesAsync() > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo insertar el autor del libro");
            }
        }
        


        }

    }

