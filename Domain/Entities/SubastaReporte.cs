using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Value_Object;

namespace Domain.Entities
{
    public class SubastaReporte
    {
        public Guid IdSubasta { get; set; }
        public NombreSubastaVO NombreSubasta { get; set; }
        public DescripcionSubastaVO DescripcionSubasta { get; set; }
        public FechaInicioSubastaVO FechaInicioSubasta { get; set; }
        public FechaFinSubastaVO FechaFinSubasta { get; set; }
        public IncrementoMinimoSubastaVO IncrementoMinimoSubasta { get; set; }
        public PrecioReservaSubastaVO PrecioReservaSubasta { get; set; }
        public EstadoSubastaVO EstadoSubasta { get; set; }
        public Guid IdUsuario { get; set; }
        public Guid IdProducto { get; set; }
        public NombreProductoVO NombreProducto { get; set; }
        public DescripcionProductoVO DescripcionProducto { get; set; }
        public ImagenURLProductoVO ImagenURLProducto { get; set; }

        public PrecioBaseProductoVO PrecioBaseProducto { get; set; }

        public CategoriaProductoVO CategoriaProducto { get; set; }

        public EstadoProductoVO EstadoProducto { get; set; }

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
