using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Exception;
using Application.Handler;
using Application.Query;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Value_Object;
using Domain.Value_Objects;
using Moq;

namespace TestMicroservicioReportes.HandlerTest
{
    public class ReportePujasRealizadasUsuarioHandlerTest
    {
        private readonly Mock<IPujaService> _pujaServiceMock = new();
        private readonly Mock<IUsuarioService> _usuarioServiceMock = new();
        private readonly ReportePujasRealizadasUsuarioHandler _handler;

        public ReportePujasRealizadasUsuarioHandlerTest()
        {
            _handler = new ReportePujasRealizadasUsuarioHandler(
                _pujaServiceMock.Object,
                _usuarioServiceMock.Object
            );
        }

        [Fact]
        public async Task Handle_DeberiaRetornarDTO_CuandoTodoEsCorrecto()
        {
            var correo = "usuario@correo.com";
            var idUsuario = Guid.NewGuid();
            var idSubasta = Guid.NewGuid();

            var puja = new Puja(
                Guid.NewGuid(),
                new MontoPujaVO(200),
                new MontoMaximoPujaVO(300),
                new TipoPujaVO("Manual"),
                new MontoPredeterminadoPujaVO(20),
                new FechaPujaVO(DateTime.UtcNow)
            );

            var reporte = new ReportePujasUsuario(
                idSubasta,
                new NombreSubastaVO("Subasta Test"),
                new List<Puja> { puja }
            );

            _usuarioServiceMock.Setup(x =>
                x.ObtenerUsuarioPorIdAsync(correo)).ReturnsAsync(idUsuario);

            _pujaServiceMock.Setup(x =>
                x.ObtenerReportePujasPorUsuarioAsync(correo))
                .ReturnsAsync(new List<ReportePujasUsuario> { reporte });

            var request = new ReportePujasRealizadasUsuarioQuery(correo);

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.Single(result);
            Assert.Equal("Subasta Test", result[0].NombreSubasta);
            Assert.Equal(idSubasta, result[0].IdSubasta);
            Assert.Single(result[0].Pujas);
            Assert.Equal(200, result[0].Pujas[0].montoPuja);
            Assert.Equal("Manual", result[0].Pujas[0].tipoPuja);
        }

        [Fact]
        public async Task Handle_DeberiaLanzarUsuarioNoEncontradoException_CuandoIdUsuarioEsEmpty()
        {
            var correo = "noexiste@correo.com";

            _usuarioServiceMock.Setup(x =>
                x.ObtenerUsuarioPorIdAsync(correo)).ReturnsAsync(Guid.Empty);

            var request = new ReportePujasRealizadasUsuarioQuery(correo);

            await Assert.ThrowsAsync<UsuarioNoEncontradoException>(() =>
                _handler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_DeberiaLanzarFalloAlObtenerPujaException_CuandoServicioFalla()
        {
            var correo = "error@correo.com";
            var idUsuario = Guid.NewGuid();

            _usuarioServiceMock.Setup(x =>
                x.ObtenerUsuarioPorIdAsync(correo)).ReturnsAsync(idUsuario);

            _pujaServiceMock.Setup(x =>
                x.ObtenerReportePujasPorUsuarioAsync(correo))
                .ThrowsAsync(new Exception("Fallo interno"));

            var request = new ReportePujasRealizadasUsuarioQuery(correo);

            await Assert.ThrowsAsync<FalloAlObtenerPujaException>(() =>
                _handler.Handle(request, CancellationToken.None));
        }

    }
}
