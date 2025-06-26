using AppRivera.Models;
using AppRivera.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppRivera.ViewModels
{
    public class TareoViewModel:INotifyCollectionChanged
    {
        public ObservableCollection<TareoModel> Tareos { get; set; } = new();

        #region columnas de la tabla (modelo)
        public int Id { get; set; }
        public string? CoProy { get; set; }
        public string? deAct { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime horaIni { get; set; }
        public DateTime horaFin { get; set; }
        #endregion

        #region Listar todo
        public ICommand CargarTareoComandInterfaz { get; }
        #endregion

        #region guardar
        public ICommand GuardarTareoComandInterfaz { get; }
        #endregion

        #region editar
        public ICommand EditarTareoCommandInterfaz { get; }
        #endregion

        #region eliminar
        public ICommand EliminarTareoCommandInterfaz { get; }
        public string Mensaje { get; set; }
        #endregion

        #region buscar por proyecto
        private string _textoBusqueda;
        public string TextoBusqueda
        {
            get => _textoBusqueda;
            set
            {
                if (_textoBusqueda != value)
                {
                    _textoBusqueda = value;
                    OnPropertyChanged(nameof(TextoBusqueda));
                    _ = Filtrar(); // 🔍 Filtrar al escribir
                }
            }
        }
        public ICommand FiltrarTareoCommandInterfaz { get; }
        #endregion


        private readonly DatabaseService _db;
        public string Resultado { get; set; }

        public TareoViewModel()
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "BDAppRivera.db3");
            _db = new DatabaseService(dbPath);

            CargarTareoComandInterfaz = new Command(async () => await ListarTareoAsync());

            GuardarTareoComandInterfaz = new Command(async () => await GuardarTareo());

            EditarTareoCommandInterfaz = new Command(async () => await EditarTareo());

            EliminarTareoCommandInterfaz = new Command(async () => await EliminarTareo());

            FiltrarTareoCommandInterfaz = new Command(async () => await FiltrarTareoAsync());
        }

        private async Task ListarTareoAsync()
        {
            var listar = await _db.obt();
            Tareos.Clear();

            foreach (var p in listar)
            {
                Tareos.Add(p);
            }
        }



        public event NotifyCollectionChangedEventHandler? CollectionChanged;
        void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
