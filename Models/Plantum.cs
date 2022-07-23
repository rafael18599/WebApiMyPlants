using System;
using System.Collections.Generic;

#nullable disable

namespace apiWebFlutter.Models
{
    public partial class Plantum
    {
        public int Id { get; set; }
        public string RutaImagen { get; set; }
        public string Descripcion { get; set; }
        public string EstadoPlanta { get; set; }
        public string Recomendacion { get; set; }
    }
}
