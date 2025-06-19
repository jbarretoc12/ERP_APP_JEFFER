using SQLite;
using System.IO;
using AppRivera.Models;
namespace AppRivera.Services
{
   public class DatabaseService
   {
        private SQLiteAsyncConnection _database;
        public DatabaseService()
        {
            InitializeDatabase();
        }
        private async void InitializeDatabase()
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BDAppRivera.db3");
            _database = new SQLiteAsyncConnection(databasePath);
            await _database.CreateTableAsync<ProyectoModel>();
        }
        public SQLiteAsyncConnection GetConnection()
        {
            return _database;
        }
    }
}
