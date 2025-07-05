using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Value_Object;

namespace Domain.Entities
{
    public class ReportePujasUsuario
    {
        public Guid IdSubasta { get; set; }

        public NombreSubastaVO NombreSubasta { get; set; }

        public List<Puja> ListaPujas { get; set; } = new();

        public ReportePujasUsuario(Guid idSubasta, NombreSubastaVO nombreSubasta, List<Puja> listaPujas)
        {
            IdSubasta = idSubasta;
            NombreSubasta = nombreSubasta;
            ListaPujas = listaPujas;
        }
    }
}
