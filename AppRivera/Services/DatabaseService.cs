using SQLite;
using System.IO;
using AppRivera.Models;
namespace AppRivera.Services
{
   public class DatabaseService
   {
        private readonly SQLiteAsyncConnection _database;
        public DatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<ProyectoModel>().Wait();
        }
        public Task<List<ProyectoModel>> ObtenerProyectosAsync()
        {
            return _database.Table<ProyectoModel>().ToListAsync();
        }
        public Task<int> InsertProyectoAsync(ProyectoModel proyecto)
        {
            return _database.InsertAsync(proyecto);
        }
        public async Task<int> UpdateProyectoAsync(ProyectoModel proyecto)
        {
            var proy =await _database.Table<ProyectoModel>().Where(p => p.CoProy == proyecto.CoProy).FirstOrDefaultAsync();
            if (proy == null)
            {
                return 0; // No se encontró el proyecto, no se actualiza nada
            }
            proy.DeProy = proyecto.DeProy; // Actualiza el campo CoProvCli

            return await _database.UpdateAsync(proy);
        }
        public async Task<int> EliminarPorProyeto(string coProy)
        {
            var proyecto = await _database.Table<ProyectoModel>().Where(p => p.CoProy == coProy).FirstOrDefaultAsync();
            if (proyecto == null) return 0;
            return await _database.DeleteAsync(proyecto);
        }

        public async Task<List<ProyectoModel>> BuscarProyectoAsync(string texto)
        {
            return await _database.Table<ProyectoModel>().Where(p => (p.CoProy).ToLower().Contains(texto.ToLower())).ToListAsync();
        }



    }
}
