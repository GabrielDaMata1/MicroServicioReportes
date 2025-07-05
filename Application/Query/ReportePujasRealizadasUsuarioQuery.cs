using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using MediatR;

namespace Application.Query
{
    public class ReportePujasRealizadasUsuarioQuery : IRequest<List<ReportePujasUsuarioDTO>>
    {
        public string correoUsuario { get; set; }

        public ReportePujasRealizadasUsuarioQuery(string correoUsuario) 
        {
            this.correoUsuario = correoUsuario;
        }
    }
}
