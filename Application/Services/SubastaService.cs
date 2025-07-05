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

namespace Application.Services
{
    public class SubastaService: ISubastaService
    {
        private readonly HttpClient _httpClient;

        public SubastaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<SubastaReporte>> ObtenerSubastas()
        {
            try
            {
                var response = await _httpClient.GetAsync("http://localhost:5003/api/Subastas/obtenerSubastas");

                if (!response.IsSuccessStatusCode)
                    return new List<SubastaReporte>();

                var contenido = await response.Content.ReadAsStringAsync();
                Console.WriteLine(contenido);

                var dtos = JsonSerializer.Deserialize<List<HistorialSubastasDTO>>(contenido, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (dtos == null || !dtos.Any())
                    return new List<SubastaReporte>();

                var subastas = dtos.Select(dto => new SubastaReporte(
                    idSubasta: dto.IdSubasta,
                    nombreSubasta: new NombreSubastaVO(dto.NombreSubasta),
                    descripcionSubasta: new DescripcionSubastaVO(dto.DescripcionSubasta),
                    fechaInicioSubasta: new FechaInicioSubastaVO(dto.FechaInicio),
                    fechaFinSubasta: new FechaFinSubastaVO(dto.FechaFin),
                    incrementoMinimoSubasta: new IncrementoMinimoSubastaVO(dto.incrementoMinimo),
                    precioReservaSubasta: new PrecioReservaSubastaVO(dto.precioReserva),
                    estadoSubasta: new EstadoSubastaVO(dto.Estado),
                    idUsuario: dto.idUsuario,
                    idProducto: dto.IdProducto,
                    nombreProducto: new NombreProductoVO(dto.NombreProducto),
                    descripcionProducto: new DescripcionProductoVO(dto.DescripcionProducto),
                    imagenURLProducto: new ImagenURLProductoVO(dto.urlImagen),
                    precioBaseProducto: new PrecioBaseProductoVO(dto.PrecioBase),
                    categoriaProducto: new CategoriaProductoVO(dto.Categoria),
                    estadoProducto: new EstadoProductoVO(dto.Estado) 
                )).ToList();

                return subastas;
            }
            catch (System.Exception ex)
            {
                return new List<SubastaReporte>();
            }
        }

    }
}
