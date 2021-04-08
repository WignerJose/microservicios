using AutoMapper;
using GenFu;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TiendaServicios.Api.Libro.Aplicacion;
using TiendaServicios.Api.Libro.Modelo;
using TiendaServicios.Api.Libro.Persistencia;
using Xunit;
namespace TiendaServicios.Api.Libro.Tests
{
    public class LibroServiceTest
    {

        private IEnumerable<LibreriaMaterial> ObtenerDataPrueba()
        {
            A.Configure<LibreriaMaterial>()
                .Fill(x => x.Titulo).AsArticleTitle()
                .Fill(x => x.LibreriaMaterialId, () => { return Guid.NewGuid(); });

            var lista = A.ListOf<LibreriaMaterial>(30);
            lista[0].LibreriaMaterialId = Guid.Empty;
            return lista;
        }

        private Mock<ContextoLibreria> CrearContexto()
        {
            var dataPrueba = ObtenerDataPrueba().AsQueryable();
            var dbSet = new Mock<DbSet<LibreriaMaterial>>();
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider).Returns(dataPrueba.Provider);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Expression).Returns(dataPrueba.Expression);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.ElementType).Returns(dataPrueba.ElementType);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.GetEnumerator()).Returns(dataPrueba.GetEnumerator());

            //Creamos las clases Enumerator y Enumerable

            // Ahora Invocamos y hacemos uso de esos metodos 
            dbSet.As<IAsyncEnumerable<LibreriaMaterial>>().Setup(x => x.GetAsyncEnumerator(new System.Threading.CancellationToken()))
                .Returns(new AsyncEnumerator<LibreriaMaterial>(dataPrueba.GetEnumerator()));

            var contexto = new Mock<ContextoLibreria>();
            contexto.Setup(x => x.LibreriaMaterial).Returns(dbSet.Object);
            return contexto;
        }



        [Fact]   
        public async void GetLibros()
        {

            ///System.Diagnostics.Debugger.Launch();  Nos sirve para poder debugear los tess
            // que metodo de mi microservicio libro se esta encargado
            // de realizar la consulta de libros de la base de datos

            // 1 Emular a la INSTANCIA DE ENTITY FRAMEWORK CORE - CONTEXTOLIBRERIA
            // PARA Simular las acciones y eventos de un objeto en un ambiente UnitTest
            // Utilizamos objetos de tipo Mock 

            var mockContexto = CrearContexto();

            ///2 emulamos el mapping IMapper
            var mapConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingTest()); /// AGREGAMOS LA CLASE QUE CREAMOS 
            });

            var mapper = mapConfiguration.CreateMapper();


            Consulta.Handler manejador = new Consulta.Handler(mockContexto.Object,mapper);
            Consulta.Ejecuta request = new Consulta.Ejecuta();

            var lista = await manejador.Handle(request, new System.Threading.CancellationToken());


            Assert.True(lista.Any());
        }
    }
}
