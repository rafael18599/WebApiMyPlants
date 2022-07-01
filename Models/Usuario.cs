using System;
using System.Collections.Generic;

#nullable disable

namespace apiWebFlutter.Models
{
    public partial class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
