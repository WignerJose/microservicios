using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.Api.Author.Aplicacion;
using TiendaServicios.Api.Author.Modelo;
using static TiendaServicios.Api.Author.Aplicacion.Nuevo;

namespace TiendaServicios.Api.Author.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AutorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>>  Crear(Ejecuta data)
        {
            return await _mediator.Send(data);

        }


        [HttpGet]
        public async Task<ActionResult<List<AutorLibro>>> getAutores()
        {
            return await _mediator.Send(new Consulta.ListaAutor());

        }





    }
}
