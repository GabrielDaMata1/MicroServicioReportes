using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Value_Object;

namespace Domain.Entities
{
    /// <summary>
    /// Clase Entity que representa a la entidad SubastaReporte en el dominio del sistema.
    /// </summary>
    public class SubastaReporte
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
        /// Atributo que corresponde a la descripcion de la subasta.
        /// </summary>
        public DescripcionSubastaVO DescripcionSubasta { get; set; }
        /// <summary>
        /// Atributo que corresponde a la fecha de inicio de la subasta.
        /// </summary>
        public FechaInicioSubastaVO FechaInicioSubasta { get; set; }
        /// <summary>
        /// Atributo que corresponde a la fecha fin de la subasta.
        /// </summary>
        public FechaFinSubastaVO FechaFinSubasta { get; set; }
        /// <summary>
        /// Atributo que corresponde al incremento minimo de la subasta.
        /// </summary>
        public IncrementoMinimoSubastaVO IncrementoMinimoSubasta { get; set; }
        /// <summary>
        /// Atributo que corresponde al precio de reserva de la subasta.
        /// </summary>
        public PrecioReservaSubastaVO PrecioReservaSubasta { get; set; }
        /// <summary>
        /// Atributo que corresponde al estado de la subasta.
        /// </summary>
        public EstadoSubastaVO EstadoSubasta { get; set; }
        /// <summary>
        /// Atributo que corresponde al ID del subastador.
        /// </summary>
        public Guid IdUsuario { get; set; }
        /// <summary>
        /// Atributo que corresponde al ID del producto subastado.
        /// </summary>
        public Guid IdProducto { get; set; }
        /// <summary>
        /// Atributo que corresponde al nombre del producto.
        /// </summary>
        public NombreProductoVO NombreProducto { get; set; }
        /// <summary>
        /// Atributo que corresponde a la descripción del producto.
        /// </summary>
        public DescripcionProductoVO DescripcionProducto { get; set; }
        /// <summary>
        /// Atributo que corresponde a la imagen url del producto .
        /// </summary>
        public ImagenURLProductoVO ImagenURLProducto { get; set; }
        /// <summary>
        /// Atributo que corresponde al precio base del producto.
        /// </summary>
        public PrecioBaseProductoVO PrecioBaseProducto { get; set; }
        /// <summary>
        /// Atributo que corresponde a la categoria del producto.
        /// </summary>
        public CategoriaProductoVO CategoriaProducto { get; set; }
        /// <summary>
        /// Atributo que corresponde al estado del producto.
        /// </summary>
        public EstadoProductoVO EstadoProducto { get; set; }
        /// <summary>
        /// Atributo que corresponde a la lista de objetos Puja que fueron realizadas en la subasta.
        /// </summary>
        public List<Puja> ListaPujas { get; set; } = new();


        public SubastaReporte(Guid idSubasta, NombreSubastaVO nombreSubasta, DescripcionSubastaVO descripcionSubasta, FechaInicioSubastaVO fechaInicioSubasta, 
            FechaFinSubastaVO fechaFinSubasta, IncrementoMinimoSubastaVO incrementoMinimoSubasta, PrecioReservaSubastaVO precioReservaSubasta, EstadoSubastaVO estadoSubasta, Guid idUsuario,
            Guid idProducto, NombreProductoVO nombreProducto, DescripcionProductoVO descripcionProducto, ImagenURLProductoVO imagenURLProducto, PrecioBaseProductoVO precioBaseProducto, 
            CategoriaProductoVO categoriaProducto, EstadoProductoVO estadoProducto, List<Puja> listaPujas)
        {
            IdSubasta = idSubasta;
            NombreSubasta = nombreSubasta;
            DescripcionSubasta = descripcionSubasta;
            FechaInicioSubasta = fechaInicioSubasta;
            FechaFinSubasta = fechaFinSubasta;
            IncrementoMinimoSubasta = incrementoMinimoSubasta;
            PrecioReservaSubasta = precioReservaSubasta;
            EstadoSubasta = estadoSubasta;
            IdUsuario = idUsuario;
            IdProducto = idProducto;
            NombreProducto = nombreProducto;
            DescripcionProducto = descripcionProducto;
            ImagenURLProducto = imagenURLProducto;
            PrecioBaseProducto = precioBaseProducto;
            CategoriaProducto = categoriaProducto;
            EstadoProducto = estadoProducto;
            ListaPujas = listaPujas;
        }
    }


}
