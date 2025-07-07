using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Value_Object;
using Domain.Value_Objects;

namespace Application.Services
{
    /// <summary>
    /// Clase Service que se encarga de procesar todas las operaciones sobre un pago, realizando peticiones HTTP al Microservicio Pagos.
    /// </summary>
    public class PagoService : IPagosService
    {
        /// <summary>
        /// Atributo que se encarga de procesar las solicitudes a servicios externos.
        /// </summary>
        private readonly HttpClient _httpClient;

        public PagoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        /// <summary>
        /// Método que se encarga de obtener los pagos recibidos por un subastador en el Microservicio Pagos.
        /// </summary>
        /// <param name="correo">Parametro que corresponde al correo del subastador a consultar</param>
        /// <returns>Retorna una lista de objetos ReportePagosRecibidosSubastador con su detalle</returns>
        public async Task<List<ReportePagosRecibidosSubastador>> ObtenerPagosRecibidos(string correo)
        {
            try
            {
                var response = await _httpClient.GetAsync($"http://localhost:5005/api/Pagos/obtenetHistorialPagosSubastador/{correo}");

                if (!response.IsSuccessStatusCode)
                    return new List<ReportePagosRecibidosSubastador>();

                var contenido = await response.Content.ReadAsStringAsync();
                Console.WriteLine(contenido);

                var dtos = JsonSerializer.Deserialize<List<HistorialPagosDTO>>(contenido, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (dtos == null || !dtos.Any())
                    return new List<ReportePagosRecibidosSubastador>();

                var pagos = dtos.Select(dto => new ReportePagosRecibidosSubastador(
                    idSubasta: dto.IdSubasta,
                    nombreSubasta: new NombreSubastaVO(dto.NombreSubasta),
                    descripcionSubasta: new DescripcionSubastaVO(dto.DescripcionSubasta),
                    fechaInicioSubasta: new FechaInicioSubastaVO(dto.FechaInicio),
                    fechaFinSubasta: new FechaFinSubastaVO(dto.FechaFin),
                    incrementoMinimoSubasta: new IncrementoMinimoSubastaVO(dto.IncrementoMinimo),
                    precioReservaSubasta: new PrecioReservaSubastaVO(dto.PrecioReserva),
                    estadoSubasta: new EstadoSubastaVO(dto.Estado),
                    idPago: dto.Id,
                    montoPago : new MontoHistorialPagosVO(dto.MontoPago),
                    fechaPago: new FechaPagoVO(dto.CreatedAt),
                    ultimosCuatroDigitosTarjetaPago: new UltimosCuatroDigitosTarjetaPagoVO(dto.UltimosDigitosTarjeta),
                    idUsuario : dto.IdUsuario
                )).ToList();

                return pagos;
            }
            catch (System.Exception ex)
            {
                return new List<ReportePagosRecibidosSubastador>();
            }
        }
    }
}
