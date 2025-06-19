using SQLite;
namespace AppRivera.Models
{
    public class ProyectoModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string? CoProy { get; set; }
        public string? CoProvCli { get; set; }

    }
}
