using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Value_Objects;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    /// <summary>
    /// Clase Entity que representa a la entidad Puja en el dominio del sistema.
    /// </summary>
    public class Puja
    {
        /// <summary>
        /// Atributo que corresponde al ID de la puja .
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Atributo que corresponde al monto de la puja .
        /// </summary>
        public MontoPujaVO MontoPuja { get; set; }
        /// <summary>
        /// Atributo que corresponde al monto maximo de la puja .
        /// </summary>
        public MontoMaximoPujaVO MontoMaximo { get; set; }
        /// <summary>
        /// Atributo que corresponde al tipo de la puja .
        /// </summary>
        public TipoPujaVO TipoPuja { get; set; }
        /// <summary>
        /// Atributo que corresponde al monto predeterminado de la puja .
        /// </summary>
        public MontoPredeterminadoPujaVO MontoPredeterminado { get; set; }
        /// <summary>
        /// Atributo que corresponde a la fecha donde se registró la puja .
        /// </summary>
        public FechaPujaVO FechaPuja { get; set; }
        /// <summary>
        /// Atributo que corresponde al correo del usuario que registró la puja .
        /// </summary>
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
