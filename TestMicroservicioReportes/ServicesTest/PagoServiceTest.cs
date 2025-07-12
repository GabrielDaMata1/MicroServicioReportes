using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Services;
using Moq.Protected;
using Moq;

namespace TestMicroservicioReportes.ServicesTest
{
    public class PagoServiceTest
    {
        [Fact]
        public async Task ObtenerPagosRecibidos_CuandoRespuestaExitosa_DeberiaRetornarPagos()
        {
            var correo = "subastador@correo.com";
            var dtos = new List<HistorialPagosDTO>
        {
            new HistorialPagosDTO
            {
                IdSubasta = Guid.NewGuid(),
                NombreSubasta = "Subasta Uno",
                DescripcionSubasta = "Descripción",
                FechaInicio = DateTime.UtcNow.AddDays(-5),
                FechaFin = DateTime.UtcNow.AddDays(-1),
                IncrementoMinimo = 50,
                PrecioReserva = 500,
                Estado = "Completed",
                Id = Guid.NewGuid(),
                MontoPago = 600,
                CreatedAt = DateTime.UtcNow,
                UltimosDigitosTarjeta = "1234",
                IdUsuario = Guid.NewGuid()
            }
        };

            var json = JsonSerializer.Serialize(dtos);
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.Method == HttpMethod.Get &&
                        req.RequestUri.ToString().Contains(correo)),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            var httpClient = new HttpClient(handlerMock.Object);
            var service = new PagoService(httpClient);

            var result = await service.ObtenerPagosRecibidos(correo);

            Assert.Single(result);
            Assert.Equal("Subasta Uno", result[0].NombreSubasta.Nombre);
            Assert.Equal("1234", result[0].UltimosCuatroDigitosTarjetaPago.ultimosCuatroDigitosTarjetaPago);
            Assert.Equal("Completed", result[0].EstadoSubasta.estado);
        }

        [Fact]
        public async Task ObtenerPagosRecibidos_CuandoRespuestaNoExitosa_DeberiaRetornarVacio()
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.BadRequest));

            var httpClient = new HttpClient(handlerMock.Object);
            var service = new PagoService(httpClient);

            var result = await service.ObtenerPagosRecibidos("correo@invalido.com");

            Assert.Empty(result);
        }

        [Fact]
        public async Task ObtenerPagosRecibidos_CuandoContenidoMalformado_DeberiaRetornarVacio()
        {
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("contenido_malformado", Encoding.UTF8, "application/json")
            };

            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            var httpClient = new HttpClient(handlerMock.Object);
            var service = new PagoService(httpClient);

            var result = await service.ObtenerPagosRecibidos("correo@error.com");

            Assert.Empty(result);
        }

    }
}
