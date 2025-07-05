using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using MediatR;

namespace Application.Query
{
    public class ReportePagosRecibidosSubastadorQuery : IRequest<List<ReportePagosRecibidosSubastadorDTO>>
    {
        public string correoUsuario { get; set; }

        public ReportePagosRecibidosSubastadorQuery(string correoUsuario)
        {
            this.correoUsuario = correoUsuario;
        }
    }
}
