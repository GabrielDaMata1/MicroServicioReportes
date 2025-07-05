using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Value_Objects
{
    public class FechaPagoVO
    {
        public DateTime fechaPago { get; set; }

        public FechaPagoVO(DateTime fechaPago)
        {
            this.fechaPago = fechaPago;
        }
    }
}
