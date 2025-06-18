using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRivera.Models
{
    public class LoginRequest
    {
        public string? Correo { get; set; }
        public string? Clave { get; set; }
    }
}
