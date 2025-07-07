using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using MediatR;

namespace Application.Query
{
    /// <summary>
    /// Clase Query que se encarga de enviar la solicitud para consultar el reporte de subastas realizadas y sus pujas.
    /// </summary>
    public class ReporteSubastasRealizadasQuery : IRequest<List<ReporteSubastasDTO>>

    {
    }
}
