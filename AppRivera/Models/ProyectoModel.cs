using SQLite;
namespace AppRivera.Models
{
    public class ProyectoModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string? CoProy { get; set; }
        public string? DeProy { get; set; }
        public bool IsSelected { get; internal set; }
    }
}
