using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Query;
using MediatR;
using MicroservicioReportes.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TestMicroservicioReportes.WebAPITest
{
    public class ReportesControllerTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly ReportesController _controller;

        public ReportesControllerTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new ReportesController(_mediatorMock.Object);
        }

        [Fact]
        public async Task ObtenerReporteSubastas_DeberiaRetornarOkConDatos()
        {
            var resultadoSimulado = new List<ReporteSubastasDTO>
        {
            new ReporteSubastasDTO { NombreSubasta = "Subasta X" }
        };

            _mediatorMock.Setup(m => m.Send(It.IsAny<ReporteSubastasRealizadasQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(resultadoSimulado);

            var response = await _controller.ObtenerReporteSubastas();

            var okResult = Assert.IsType<OkObjectResult>(response);
            var data = Assert.IsAssignableFrom<List<ReporteSubastasDTO>>(okResult.Value);
            Assert.Single(data);
            Assert.Equal("Subasta X", data[0].NombreSubasta);
        }

        [Fact]
        public async Task ObtenerReportePujasRealizadasUsuario_DeberiaRetornarOkConDatos()
        {
            var correo = "usuario@correo.com";
            var resultadoSimulado = new List<ReportePujasUsuarioDTO>
        {
            new ReportePujasUsuarioDTO { NombreSubasta = "Subasta Y" }
        };

            _mediatorMock.Setup(m => m.Send(It.IsAny<ReportePujasRealizadasUsuarioQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(resultadoSimulado);

            var response = await _controller.ObtenerReportePujasRealizadasUsuario(correo);

            var okResult = Assert.IsType<OkObjectResult>(response);
            var data = Assert.IsAssignableFrom<List<ReportePujasUsuarioDTO>>(okResult.Value);
            Assert.Single(data);
            Assert.Equal("Subasta Y", data[0].NombreSubasta);
        }

        [Fact]
        public async Task ObtenerPagosRecibidosSubastador_DeberiaRetornarOkConDatos()
        {
            var correo = "subastador@correo.com";
            var resultadoSimulado = new List<ReportePagosRecibidosSubastadorDTO>
        {
            new ReportePagosRecibidosSubastadorDTO { NombreSubasta = "Subasta Z" }
        };

            _mediatorMock.Setup(m => m.Send(It.IsAny<ReportePagosRecibidosSubastadorQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(resultadoSimulado);

            var response = await _controller.ObtenerPagosRecibidosSubastador(correo);

            var okResult = Assert.IsType<OkObjectResult>(response);
            var data = Assert.IsAssignableFrom<List<ReportePagosRecibidosSubastadorDTO>>(okResult.Value);
            Assert.Single(data);
            Assert.Equal("Subasta Z", data[0].NombreSubasta);
        }

    }
}
