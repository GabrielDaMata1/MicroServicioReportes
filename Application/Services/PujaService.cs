using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Exception;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Value_Object;
using Domain.Value_Objects;

namespace Application.Services
{
    /// <summary>
    /// Clase Service que se encarga de procesar todas las operaciones sobre una puja, realizando peticiones HTTP al Microservicio Puja.
    /// </summary>
    public class PujaService : IPujaService
    {
        /// <summary>
        /// Atributo que se encarga de procesar las solicitudes a servicios externos.
        /// </summary>
        private readonly HttpClient _httpClient;

    public PujaService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
        /// <summary>
        /// Método que se encarga de obtener las pujas agrupadas por subastas realziads por un usuario en el Microservicio Pujas.
        /// </summary>
        /// <param name="correo">Parametro que corresponde al correo del usuario a consultar</param>
        /// <returns>Retorna una lista de objetos ReportePujaUsuario con su detalle</returns>
        public async Task<List<ReportePujasUsuario>> ObtenerReportePujasPorUsuarioAsync(string correo)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5004/api/Pujas/obtenerPujasUsuario/{correo}");

            if (!response.IsSuccessStatusCode)
                throw new FalloAlObtenerPujaException($"No se pudo obtener el reporte de pujas del usuario {correo}");

            var contenido = await response.Content.ReadAsStringAsync();

            var dtos = JsonSerializer.Deserialize<List<HistorialPujasUsuarioDTO>>(contenido, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (dtos == null || !dtos.Any())
                return new List<ReportePujasUsuario>();

            var resultado = dtos.Select(dto => new ReportePujasUsuario(
                idSubasta: dto.IdSubasta,
                nombreSubasta: new NombreSubastaVO(dto.NombreSubasta),
                listaPujas: dto.Pujas.Select(p => new Puja(
                    id: p.id,
                    montoPuja:new MontoPujaVO(p.montoPuja),
                    montoMaximo: new MontoMaximoPujaVO(p.montoMaximo),
                    tipoPuja: new TipoPujaVO(p.tipoPuja),
                    montoPredeterminado: new MontoPredeterminadoPujaVO(p.montoPredeterminado),
                    fechaPuja: new FechaPujaVO(p.fecha)
                )).ToList()
            )).ToList();

            return resultado;
        }

    }
}
