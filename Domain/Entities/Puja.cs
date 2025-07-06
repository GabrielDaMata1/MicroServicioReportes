using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Value_Objects;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Puja
    {
        public Guid Id { get; set; }

        public MontoPujaVO MontoPuja { get; set; }

        public MontoMaximoPujaVO MontoMaximo { get; set; }

        public TipoPujaVO TipoPuja { get; set; }

        public MontoPredeterminadoPujaVO MontoPredeterminado { get; set; }

        public FechaPujaVO FechaPuja { get; set; } 

        public string CorreoUsuarioPuja { get; set; }

        [JsonConstructor]
        public Puja(Guid id, MontoPujaVO montoPuja, MontoMaximoPujaVO montoMaximo, TipoPujaVO tipoPuja, MontoPredeterminadoPujaVO montoPredeterminado, FechaPujaVO fechaPuja)
        {
            Id = id;
            MontoPuja = montoPuja;
            MontoMaximo = montoMaximo;
            TipoPuja = tipoPuja;
            MontoPredeterminado = montoPredeterminado;
            FechaPuja = fechaPuja;
        }

        public Puja(Guid id, MontoPujaVO montoPuja, MontoMaximoPujaVO montoMaximo, TipoPujaVO tipoPuja, MontoPredeterminadoPujaVO montoPredeterminado, FechaPujaVO fechaPuja, string correoUsuario)
        {
            Id = id;
            MontoPuja = montoPuja;
            MontoMaximo = montoMaximo;
            TipoPuja = tipoPuja;
            MontoPredeterminado = montoPredeterminado;
            FechaPuja = fechaPuja;
            CorreoUsuarioPuja = correoUsuario;
        }
    }

    
}
