using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Services;
using Moq.Protected;
using Moq;
using System.Text.Json;

namespace TestMicroservicioReportes.ServicesTest
{
    public class SubastaServiceTest
    {
        [Fact]
        public async Task ObtenerSubastas_RespuestaExitosa_DeberiaRetornarListaSubastas()
        {
            var dto = new HistorialSubastasDTO
            {
                IdSubasta = Guid.NewGuid(),
                NombreSubasta = "Subasta 1",
                DescripcionSubasta = "Descripción",
                FechaInicio = DateTime.UtcNow.AddDays(-5),
                FechaFin = DateTime.UtcNow.AddDays(-1),
                incrementoMinimo = 10,
                precioReserva = 500,
                Estado = "Finalizada",
                idUsuario = Guid.NewGuid(),
                IdProducto = Guid.NewGuid(),
                NombreProducto = "Producto 1",
                DescripcionProducto = "Desc",
                urlImagen = "http://imagen.png",
                PrecioBase = 400,
                Categoria = "Electrónica",
                Pujas = new List<PujaDTO>
            {
                new PujaDTO
                {
                    id = Guid.NewGuid(),
                    montoPuja = 600,
                    montoMaximo = 700,
                    tipoPuja = "Manual",
                    montoPredeterminado = 50,
                    fecha = DateTime.UtcNow,
                    correoUsuario = "usuario@correo.com"
                }
            }
            };

            var json = JsonSerializer.Serialize(new List<HistorialSubastasDTO> { dto });
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.Is<HttpRequestMessage>(r =>
                        r.Method == HttpMethod.Get &&
                        r.RequestUri.ToString().Contains("obtenerSubastasGanadasPujas")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            var httpClient = new HttpClient(handler.Object);
            var service = new SubastaService(httpClient);

            var result = await service.ObtenerSubastas();

            Assert.Single(result);
            Assert.Equal("Subasta 1", result[0].NombreSubasta.Nombre);
            Assert.Single(result[0].ListaPujas);
            Assert.Equal(600, result[0].ListaPujas[0].MontoPuja.montoPuja);
        }

        [Fact]
        public async Task ObtenerSubastas_RespuestaConError_DeberiaRetornarListaVacia()
        {
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            var handler = new Mock<HttpMessageHandler>();

            handler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            var httpClient = new HttpClient(handler.Object);
            var service = new SubastaService(httpClient);

            var result = await service.ObtenerSubastas();

            Assert.Empty(result);
        }

        [Fact]
        public async Task ObtenerSubastas_ContenidoInvalido_DeberiaRetornarListaVacia()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("contenido_malformado", Encoding.UTF8, "application/json")
            };

            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            var httpClient = new HttpClient(handler.Object);
            var service = new SubastaService(httpClient);

            var result = await service.ObtenerSubastas();

            Assert.Empty(result);
        }

    }
}
