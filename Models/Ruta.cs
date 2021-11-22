using System;
using System.Collections.Generic;

#nullable disable

namespace TrenCr.Models
{
    public partial class Ruta
    {
        public Ruta()
        {
            CompraBoletos = new HashSet<CompraBoleto>();
            Estacions = new HashSet<Estacion>();
        }

        public int IdRuta { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<CompraBoleto> CompraBoletos { get; set; }
        public virtual ICollection<Estacion> Estacions { get; set; }
    }
}
