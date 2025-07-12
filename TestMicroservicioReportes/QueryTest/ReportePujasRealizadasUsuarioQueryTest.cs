using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Query;
using MediatR;

namespace TestMicroservicioReportes.QueryTest
{
    public class ReportePujasRealizadasUsuarioQueryTest
    {
        [Fact]
        public void Constructor_DeberiaAsignarCorreoCorrectamente()
        {
            var correo = "usuario@correo.com";
            var query = new ReportePujasRealizadasUsuarioQuery(correo);

            Assert.Equal(correo, query.correoUsuario);
        }

        [Fact]
        public void Query_DeberiaSerTipoDeRequestConListaDTO()
        {
            var query = new ReportePujasRealizadasUsuarioQuery("test@correo.com");

            Assert.IsAssignableFrom<IRequest<List<ReportePujasUsuarioDTO>>>(query);
        }

    }
}
