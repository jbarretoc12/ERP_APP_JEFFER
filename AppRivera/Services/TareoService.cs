using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppRivera.Models;

namespace AppRivera.Services
{
    public class TareoService
    {
        private readonly SQLiteAsyncConnection _database;
        public TareoService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<TareoModel>().Wait();
        }
        public Task<List<TareoModel>> ObtenerTareosAsync()
        {
            return _database.Table<TareoModel>().ToListAsync();
        }
        public Task<int> InsertTareosAsync(TareoModel tareo)
        {
            return _database.InsertAsync(tareo);
        }

        public async Task<int> UpdateTareosAsync(TareoModel tareo)
        {
            var tar = await _database.Table<TareoModel>().Where(p => p.Id == tareo.Id).FirstOrDefaultAsync();
            if (tar == null)
            {
                return 0; // No se encontró el proyecto, no se actualiza nada
            }
            tar.CoProy = tareo.CoProy; // Actualiza el campo CoProvCli
            tar.Fecha=tareo.Fecha;
            tar.DeAct = tareo.DeAct; // Actualiza el campo deAct
            tar.horaIni = tareo.horaIni; // Actualiza el campo horaIni
            tar.horaFin = tareo.horaFin; // Actualiza el campo horaFin


            return await _database.UpdateAsync(tar);
        }

        public async Task<int> EliminarTareo(int id)
        {
            var tar = await _database.Table<TareoModel>().Where(p => p.Id == id).FirstOrDefaultAsync();
            if (tar == null) return 0;
            return await _database.DeleteAsync(tar);
        }

       /* public async Task<List<TareoModel>> BuscarTareoAsync(string texto)
        {
            return await _database.Table<TareoModel>().Where(p => (p.deAct).ToLower().Contains(texto.ToLower())).ToListAsync();
        }*/
    }
}
