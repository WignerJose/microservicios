using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.Aplicacion;
using static TiendaServicios.Api.Libro.Aplicacion.Nuevo;

namespace TiendaServicios.Api.Libro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroMaterialController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LibroMaterialController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<ActionResult<Unit>> crear(Ejecuta data)
        {
            return await _mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<List<LibroMaterialDto>>> getLibreriaMaterial()
        {
            return await _mediator.Send(new Consulta.Ejecuta());
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<LibroMaterialDto>> getLibreriaMaterial(Guid id)
        {
            return await _mediator.Send(new ConsultaFilter.LibroUnico{LibroId=id });
        }
    }
}
