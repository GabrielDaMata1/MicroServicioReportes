using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Value_Objects
{
    public class UltimosCuatroDigitosTarjetaPagoVO
    {
        public string ultimosCuatroDigitosTarjetaPago { get; set; }

        public UltimosCuatroDigitosTarjetaPagoVO(string ultimosCuatroDigitosTarjetaPago)
        {
            this.ultimosCuatroDigitosTarjetaPago = ultimosCuatroDigitosTarjetaPago;
        }
    }
}
