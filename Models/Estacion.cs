using System;
using System.Collections.Generic;

#nullable disable

namespace TrenCr.Models
{
    public partial class Estacion
    {
        public Estacion()
        {
            CompraBoletos = new HashSet<CompraBoleto>();
        }

        public int IdEstacion { get; set; }
        public int? IdRuta { get; set; }
        public string NomEstacion { get; set; }
        public DateTime? Horario { get; set; }
        public int? EspaciosDisponibles { get; set; }
        public int? CantBoletos { get; set; }

        public virtual Ruta IdRutaNavigation { get; set; }
        public virtual ICollection<CompraBoleto> CompraBoletos { get; set; }
    }
}
