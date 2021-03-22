using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.Api.Author.Modelo;

namespace TiendaServicios.Api.Author.Aplicacion
{
    public class MappingProfile:Profile 
    {
        public MappingProfile()
        {
            CreateMap<AutorLibro, AutorDto>();
        }

    }
}
