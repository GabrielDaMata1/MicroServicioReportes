using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Exception;
using Application.Services;
using Moq.Protected;
using Moq;

namespace TestMicroservicioReportes.ServicesTest
{
    public class PujaServiceTest
    {
        [Fact]
        public async Task ObtenerReportePujasPorUsuarioAsync_RespuestaExitosa_DeberiaRetornarPujas()
        {
            var correo = "usuario@correo.com";
            var dtos = new List<HistorialPujasUsuarioDTO>
        {
            new HistorialPujasUsuarioDTO
            {
                IdSubasta = Guid.NewGuid(),
                NombreSubasta = "Subasta Uno",
                Pujas = new List<PujaDTO>
                {
                    new PujaDTO
                    {
                        id = Guid.NewGuid(),
                        montoPuja = 100,
                        montoMaximo = 150,
                        tipoPuja = "Manual",
                        montoPredeterminado = 20,
                        fecha = DateTime.UtcNow
                    }
                }
            }
        };

            var json = JsonSerializer.Serialize(dtos);
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.Is<HttpRequestMessage>(r =>
                        r.Method == HttpMethod.Get &&
                        r.RequestUri.ToString().Contains(correo)),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            var httpClient = new HttpClient(handler.Object);
            var service = new PujaService(httpClient);

            var result = await service.ObtenerReportePujasPorUsuarioAsync(correo);

            Assert.Single(result);
            Assert.Equal("Subasta Uno", result[0].NombreSubasta.Nombre);
            Assert.Single(result[0].ListaPujas);
            Assert.Equal(100, result[0].ListaPujas[0].MontoPuja.montoPuja);
        }

        [Fact]
        public async Task ObtenerReportePujasPorUsuarioAsync_RespuestaConContenidoVacio_DeberiaRetornarListaVacia()
        {
            var correo = "usuario@vacio.com";
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("[]", Encoding.UTF8, "application/json")
            };

            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.Is<HttpRequestMessage>(r => r.RequestUri.ToString().Contains(correo)),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            var httpClient = new HttpClient(handler.Object);
            var service = new PujaService(httpClient);

            var result = await service.ObtenerReportePujasPorUsuarioAsync(correo);

            Assert.Empty(result);
        }

        [Fact]
        public async Task ObtenerReportePujasPorUsuarioAsync_RespuestaError_DeberiaLanzarExcepcion()
        {
            var correo = "usuario@error.com";
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest);

            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.Is<HttpRequestMessage>(r => r.RequestUri.ToString().Contains(correo)),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            var httpClient = new HttpClient(handler.Object);
            var service = new PujaService(httpClient);

            await Assert.ThrowsAsync<FalloAlObtenerPujaException>(() =>
                service.ObtenerReportePujasPorUsuarioAsync(correo));
        }


    }
}
