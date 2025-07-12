using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Exception;
using Application.Exceptions;
using Application.Handler;
using Application.Query;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Value_Object;
using Domain.Value_Objects;
using Moq;

namespace TestMicroservicioReportes.HandlerTest
{
    public class ReporteSubastasRealizadasHandlerTest
    {
        private readonly Mock<ISubastaService> _subastaServiceMock = new();
        private readonly Mock<IUsuarioService> _usuarioServiceMock = new();
        private readonly ReporteSubastasRealizadasHandler _handler;

        public ReporteSubastasRealizadasHandlerTest()
        {
            _handler = new ReporteSubastasRealizadasHandler(_subastaServiceMock.Object, _usuarioServiceMock.Object);
        }

        [Fact]
        public async Task Handle_DeberiaRetornarDTO_CuandoSubastasYUsuariosSonValidos()
        {
            var idSubasta = Guid.NewGuid();
            var idProducto = Guid.NewGuid();
            var idUsuario = Guid.NewGuid();

            var subasta = new SubastaReporte(
                idSubasta,
                new NombreSubastaVO("Subasta Uno"),
                new DescripcionSubastaVO("Descripción de prueba"),
                new FechaInicioSubastaVO(DateTime.UtcNow.AddDays(-10)),
                new FechaFinSubastaVO(DateTime.UtcNow.AddDays(-5)),
                new IncrementoMinimoSubastaVO(100),
                new PrecioReservaSubastaVO(1000),
                new EstadoSubastaVO("Finalizada"),
                idUsuario,
                idProducto,
                new NombreProductoVO("Smartphone"),
                new DescripcionProductoVO("Android"),
                new ImagenURLProductoVO("http://imagen.png"),
                new PrecioBaseProductoVO(800),
                new CategoriaProductoVO("Electrónica"),
                new EstadoProductoVO("Disponible"),
                new List<Puja>
                {
                new Puja(Guid.NewGuid(), new MontoPujaVO(1200), new MontoMaximoPujaVO(1300),
                    new TipoPujaVO("Manual"), new MontoPredeterminadoPujaVO(100), new FechaPujaVO(DateTime.UtcNow), "usuario@correo.com")
                }
            );

            _subastaServiceMock.Setup(x => x.ObtenerSubastas()).ReturnsAsync(new List<SubastaReporte> { subasta });
            _usuarioServiceMock.Setup(x => x.ObtenerCorreoPorIdAsync(idUsuario)).ReturnsAsync("usuario@correo.com");

            var query = new ReporteSubastasRealizadasQuery();
            var resultado = await _handler.Handle(query, CancellationToken.None);

            Assert.Single(resultado);
            var dto = resultado[0];
            Assert.Equal("usuario@correo.com", dto.correoUsuario);
            Assert.Equal("Subasta Uno", dto.NombreSubasta);
            Assert.Equal("Smartphone", dto.NombreProducto);
            Assert.Single(dto.Pujas);
            Assert.Equal("Manual", dto.Pujas[0].tipoPuja);
        }

        [Fact]
        public async Task Handle_DeberiaLanzarUsuarioNoEncontradoException_CuandoCorreoNoExiste()
        {
            var subasta = new SubastaReporte(
                Guid.NewGuid(),
                new NombreSubastaVO("Subasta Fallida"),
                new DescripcionSubastaVO("Error"),
                new FechaInicioSubastaVO(DateTime.UtcNow.AddDays(-2)),
                new FechaFinSubastaVO(DateTime.UtcNow.AddDays(-1)),
                new IncrementoMinimoSubastaVO(20),
                new PrecioReservaSubastaVO(100),
                new EstadoSubastaVO("Finalizada"),
                Guid.NewGuid(),
                Guid.NewGuid(),
                new NombreProductoVO("Tablet"),
                new DescripcionProductoVO("Samsung"),
                new ImagenURLProductoVO("url"),
                new PrecioBaseProductoVO(300),
                new CategoriaProductoVO("Tecnología"),
                new EstadoProductoVO("Disponible"),
                new List<Puja>()
            );

            _subastaServiceMock.Setup(x => x.ObtenerSubastas()).ReturnsAsync(new List<SubastaReporte> { subasta });
            _usuarioServiceMock.Setup(x => x.ObtenerCorreoPorIdAsync(subasta.IdUsuario)).ReturnsAsync((string)null);

            var query = new ReporteSubastasRealizadasQuery();

            await Assert.ThrowsAsync<UsuarioNoEncontradoException>(() =>
                _handler.Handle(query, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_DeberiaLanzarFalloAlObtenerSubastasException_CuandoServicioSubastaFalla()
        {
            _subastaServiceMock.Setup(x => x.ObtenerSubastas())
                .ThrowsAsync(new Exception("Error interno"));

            var query = new ReporteSubastasRealizadasQuery();

            await Assert.ThrowsAsync<FalloAlObtenerSubastasException>(() =>
                _handler.Handle(query, CancellationToken.None));
        }

    }
}
