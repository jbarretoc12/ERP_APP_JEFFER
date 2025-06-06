using System;
using System.Collections.Generic;

namespace ServicioApi.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? Nonbre { get; set; }

    public string? Correo { get; set; }

    public string? Clave { get; set; }
}
