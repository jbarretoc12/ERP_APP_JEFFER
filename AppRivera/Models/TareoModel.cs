using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRivera.Models
{
    public class TareoModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string? CoProy { get; set; }
        public string? deAct { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime horaIni { get; set; }
        public DateTime horaFin { get; set; }

    }
}
