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
    public class ReportePagosRecibidosSubastadorHandlerTest
    {
        private readonly Mock<IPagosService> _pagosServiceMock = new();
        private readonly Mock<IUsuarioService> _usuarioServiceMock = new();
        private readonly ReportePagosRecibidosSubastadorHandler _handler;

        public ReportePagosRecibidosSubastadorHandlerTest()
        {
            _handler = new ReportePagosRecibidosSubastadorHandler(
                _pagosServiceMock.Object,
                _usuarioServiceMock.Object
            );
        }

        [Fact]
        public async Task Handle_DeberiaRetornarDTO_CuandoTodoExitoso()
        {
            var correoSubastador = "subastador@correo.com";
            var usuarioId = Guid.NewGuid();
            var subastaId = Guid.NewGuid();
            var pagoId = Guid.NewGuid();

            var pago = new ReportePagosRecibidosSubastador(
                subastaId,
                new NombreSubastaVO("Subasta Premium"),
                new DescripcionSubastaVO("Arte digital"),
                new FechaInicioSubastaVO(DateTime.UtcNow.AddDays(-10)),
                new FechaFinSubastaVO(DateTime.UtcNow.AddDays(-2)),
                new IncrementoMinimoSubastaVO(100),
                new PrecioReservaSubastaVO(1000),
                new EstadoSubastaVO("Completed"),
                pagoId,
                new MontoHistorialPagosVO(1500),
                new FechaPagoVO(DateTime.UtcNow),
                new UltimosCuatroDigitosTarjetaPagoVO("5678"),
                usuarioId
            );

            _pagosServiceMock.Setup(x => x.ObtenerPagosRecibidos(correoSubastador))
                .ReturnsAsync(new List<ReportePagosRecibidosSubastador> { pago });

            _usuarioServiceMock.Setup(x => x.ObtenerCorreoPorIdAsync(usuarioId))
                .ReturnsAsync("comprador@correo.com");

            var request = new ReportePagosRecibidosSubastadorQuery(correoSubastador);
            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.Single(result);
            var dto = result[0];
            Assert.Equal(subastaId, dto.IdSubasta);
            Assert.Equal("Subasta Premium", dto.NombreSubasta);
            Assert.Equal("comprador@correo.com", dto.correo);
            Assert.Equal("5678", dto.UltimosDigitosTarjeta);
        }

        [Fact]
        public async Task Handle_DeberiaLanzarUsuarioNoEncontradoException_CuandoCorreoNoExiste()
        {
            var usuarioId = Guid.NewGuid();
            var correoSubastador = "subastador@correo.com";

            var pago = new ReportePagosRecibidosSubastador(
                Guid.NewGuid(),
                new NombreSubastaVO("Subasta Fallida"),
                new DescripcionSubastaVO("Sin datos"),
                new FechaInicioSubastaVO(DateTime.UtcNow),
                new FechaFinSubastaVO(DateTime.UtcNow),
                new IncrementoMinimoSubastaVO(10),
                new PrecioReservaSubastaVO(100),
                new EstadoSubastaVO("Completed"),
                Guid.NewGuid(),
                new MontoHistorialPagosVO(200),
                new FechaPagoVO(DateTime.UtcNow),
                new UltimosCuatroDigitosTarjetaPagoVO("0000"),
                usuarioId
            );

            _pagosServiceMock.Setup(x => x.ObtenerPagosRecibidos(correoSubastador))
                .ReturnsAsync(new List<ReportePagosRecibidosSubastador> { pago });

            _usuarioServiceMock.Setup(x => x.ObtenerCorreoPorIdAsync(usuarioId))
                .ReturnsAsync((string)null);

            var request = new ReportePagosRecibidosSubastadorQuery(correoSubastador);

            await Assert.ThrowsAsync<UsuarioNoEncontradoException>(() =>
                _handler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_DeberiaLanzarFalloAlObtenerSubastasException_CuandoPagosServiceFalla()
        {
            var request = new ReportePagosRecibidosSubastadorQuery("error@correo.com");

            _pagosServiceMock.Setup(x =>
                x.ObtenerPagosRecibidos(request.correoUsuario))
                .ThrowsAsync(new Exception("Fallo interno"));

            await Assert.ThrowsAsync<FalloAlObtenerSubastasException>(() =>
                _handler.Handle(request, CancellationToken.None));
        }


    }
}
