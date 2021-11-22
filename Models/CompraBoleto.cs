using System;
using System.Collections.Generic;

#nullable disable

namespace TrenCr.Models
{
    public partial class CompraBoleto
    {
        public int IdCompra { get; set; }
        public int IdEstacion { get; set; }
        public int IdRuta { get; set; }
        public string Estacion { get; set; }
        public DateTime Horario { get; set; }
        public int CantBoletos { get; set; }

        public virtual Estacion IdEstacionNavigation { get; set; }
        public virtual Ruta IdRutaNavigation { get; set; }
    }
}
