using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Exception;
using Application.Exceptions;
using Application.Query;
using Domain.Interfaces;
using MediatR;

namespace Application.Handler
{
    public class ReporteSubastasRealizadasHandler : IRequestHandler<ReporteSubastasRealizadasQuery, List<ReporteSubastasDTO>>
    {
        private readonly ISubastaService _subastaService;
        private readonly IUsuarioService _usuarioService;

        public ReporteSubastasRealizadasHandler(ISubastaService subastaService, IUsuarioService usuarioService)
        {
            _subastaService = subastaService;
            _usuarioService = usuarioService;
        }
        public async Task<List<ReporteSubastasDTO>> Handle(ReporteSubastasRealizadasQuery request, CancellationToken cancellationToken)
        {
            try
            {
               var subastas = await _subastaService.ObtenerSubastas();

               var resultado = new List<ReporteSubastasDTO>();

               foreach (var subasta in subastas)
               {
                 var correo = await _usuarioService.ObtenerCorreoPorIdAsync(subasta.IdUsuario) ?? throw new UsuarioNoEncontradoException();
                 var dto = new ReporteSubastasDTO
                 {
                     IdSubasta = subasta.IdSubasta,
                     NombreSubasta = subasta.NombreSubasta.Nombre,
                     DescripcionSubasta = subasta.DescripcionSubasta.descripcion,
                     Estado = subasta.EstadoSubasta.estado,
                     FechaInicio = subasta.FechaInicioSubasta.fechaInicio,
                     FechaFin = subasta.FechaFinSubasta.fechaFin,
                     incrementoMinimo = subasta.IncrementoMinimoSubasta.incrementoMinimo,
                     precioReserva = subasta.PrecioReservaSubasta.precioReserva,
                     correoUsuario = correo,
                     IdProducto = subasta.IdProducto,
                     NombreProducto = subasta.NombreProducto.Nombre,
                     DescripcionProducto = subasta.DescripcionProducto.descripcion,
                     PrecioBase = subasta.PrecioBaseProducto.precio,
                     Categoria = subasta.CategoriaProducto.categoria,
                     urlImagen = subasta.ImagenURLProducto.url
                 };

                 resultado.Add(dto);
               }

                return resultado;
            }
            catch (System.Exception ex)
            {
               throw new FalloAlObtenerSubastasException("Error al generar el reporte de subastas", ex);
            }
        }
    }

}

