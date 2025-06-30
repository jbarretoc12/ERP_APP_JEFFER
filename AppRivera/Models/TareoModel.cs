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

        public int Item { get; set; }
        public string? CoProy { get; set; }
        public string? DeAct { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan horaIni { get; set; }
        public TimeSpan horaFin { get; set; }

    }
}
