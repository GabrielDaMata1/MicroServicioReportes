using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Exception;
using Application.Exceptions;
using Application.Query;
using Application.Services;
using Domain.Interfaces;
using MediatR;

namespace Application.Handler
{
    public class ReportePujasRealizadasUsuarioHandler : IRequestHandler<ReportePujasRealizadasUsuarioQuery, List<ReportePujasUsuarioDTO>>
    {
        private readonly IPujaService _pujaService;
        private readonly IUsuarioService _usuarioService;

        public ReportePujasRealizadasUsuarioHandler(IPujaService pujaService, IUsuarioService usuarioService)
        {
            _pujaService = pujaService;
            _usuarioService = usuarioService;
        }

        public async Task<List<ReportePujasUsuarioDTO>> Handle(ReportePujasRealizadasUsuarioQuery request, CancellationToken cancellationToken)
        {
          try
          {
            var idUsuario = await _usuarioService.ObtenerUsuarioPorIdAsync(request.correoUsuario);
            
            if (idUsuario==Guid.Empty)
               throw new UsuarioNoEncontradoException();


            var reportePujasUsuario = await _pujaService.ObtenerReportePujasPorUsuarioAsync(request.correoUsuario);

            var dtos = reportePujasUsuario.Select(e => new ReportePujasUsuarioDTO
            {
                IdSubasta = e.IdSubasta,
                NombreSubasta = e.NombreSubasta.Nombre,
                Pujas = e.ListaPujas.Select(p => new PujaDTO
                {
                    id = p.Id,
                    montoPuja = p.MontoPuja.montoPuja,
                    montoMaximo = p.MontoMaximo.montoMaximo,
                    tipoPuja = p.TipoPuja.tipoPuja,
                    montoPredeterminado = p.MontoPredeterminado.montoPredeterminado,
                    fecha = p.FechaPuja.fechaPuja
                }).ToList()
            }).ToList();

            return dtos;
          }
          catch (UsuarioNoEncontradoException)
          {
               throw;
          }
          catch (System.Exception ex)
          {
               throw new FalloAlObtenerPujaException("Error al generar el reporte de pujas", ex);
          }
        }

    }
}
