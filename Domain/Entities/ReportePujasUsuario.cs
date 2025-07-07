using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Value_Object;

namespace Domain.Entities
{
    /// <summary>
    /// Clase Entity que representa a la entidad ReportePujasUsuario en el dominio del sistema.
    /// </summary>
    public class ReportePujasUsuario
    {
        /// <summary>
        /// Atributo que corresponde al ID de la subasta.
        /// </summary>
        public Guid IdSubasta { get; set; }
        /// <summary>
        /// Atributo que corresponde al nombre de la subasta.
        /// </summary>

        public NombreSubastaVO NombreSubasta { get; set; }
        /// <summary>
        /// Atributo que corresponde a la lista de objetos Puja que fueron realizadas en la subasta.
        /// </summary>
        public List<Puja> ListaPujas { get; set; } = new();

        public ReportePujasUsuario(Guid idSubasta, NombreSubastaVO nombreSubasta, List<Puja> listaPujas)
        {
            IdSubasta = idSubasta;
            NombreSubasta = nombreSubasta;
            ListaPujas = listaPujas;
        }
    }
}
