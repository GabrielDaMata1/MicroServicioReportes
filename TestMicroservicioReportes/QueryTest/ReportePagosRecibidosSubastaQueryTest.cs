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
    public class ReportePagosRecibidosSubastaQueryTest
    {
        [Fact]
        public void Constructor_DeberiaAsignarCorreoCorrectamente()
        {
            var correo = "subastador@correo.com";
            var query = new ReportePagosRecibidosSubastadorQuery(correo);

            Assert.Equal(correo, query.correoUsuario);
        }

        [Fact]
        public void Query_DeberiaSerTipoDeRequestConListaDTO()
        {
            var query = new ReportePagosRecibidosSubastadorQuery("test@correo.com");

            Assert.IsAssignableFrom<IRequest<List<ReportePagosRecibidosSubastadorDTO>>>(query);
        }
    }

}
