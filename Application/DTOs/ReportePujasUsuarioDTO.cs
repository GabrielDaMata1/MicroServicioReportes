using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ReportePujasUsuarioDTO
    {
        public Guid IdSubasta { get; set; }
        public string NombreSubasta { get; set; }

        public List<PujaDTO> Pujas { get; set; } = new();

    }
}
