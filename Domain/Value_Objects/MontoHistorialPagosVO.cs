using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Value_Objects
{
    public class MontoHistorialPagosVO
    {
        public decimal montoPago { get; set; }

        public MontoHistorialPagosVO(decimal montoPago)
        {
            this.montoPago = montoPago;
        }
    }
}
