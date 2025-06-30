using AppRivera.Models;
using AppRivera.Services;
using AppRivera.Views;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    public class TareoViewModel:INotifyPropertyChanged
    {
        public ObservableCollection<TareoModel> Tareos { get; set; } = new();

        #region columnas de la tabla (modelo)
        public int Id { get; set; }
        public string? CoProy { get; set; }
        public string? DeAct { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan horaIni { get; set; }
        public TimeSpan horaFin { get; set; }
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
        private string _coProy;
        public string ProyectoBusqueda
        {
            get => _coProy;
            set
            {
                if (_coProy != value)
                {
                    _coProy = value;
                    OnPropertyChanged(nameof(ProyectoBusqueda));
                    _ = Filtrar(); // 🔍 Filtrar al escribir
                }
            }
        }
        public ICommand BuscarProyectosCommandInterfaz { get; }
        #endregion


        private readonly TareoService _db;
        public string Resultado { get; set; }

        public TareoViewModel()
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "BDAppRivera.db3");
            _db = new TareoService(dbPath);

            CargarTareoComandInterfaz = new Command(async () => await ListarTareoAsync());

            GuardarTareoComandInterfaz = new Command(async () => await GuardarTareo());

            EditarTareoCommandInterfaz = new Command(async () => await EditarTareo());

            EliminarTareoCommandInterfaz = new Command(async () => await EliminarTareo());

            BuscarProyectosCommandInterfaz = new Command(async () => await BuscarTareoXproyectoAsync());

            //FiltrarTareoCommandInterfaz = new Command(async () => await FiltrarTareoAsync());
        }

        private async Task ListarTareoAsync()
        {
            var listar = await _db.ObtenerTareosAsync();
            int i = 1;
            Tareos.Clear();
            foreach (var p in listar)
            {
                p.Item = i ++;
                Tareos.Add(p);
            }
        }

        private async Task GuardarTareo()
        {
            if (string.IsNullOrWhiteSpace(CoProy)) return;

            /*var proyecto=new ProyectoModel
            {
                CoProy = CoProy,
                DeProy = "" // Asumiendo que DeAct es el nombre del proyecto
            };*/
            var NuevoTareo = new TareoModel
            {
                Id = 0, // AutoIncrement, no es necesario asignar un valor
                CoProy = CoProy,
                DeAct = DeAct,
                Fecha = Fecha,
                horaIni = horaIni,
                horaFin = horaFin
            };
            await _db.InsertTareosAsync(NuevoTareo);


            /*(Application.Current.MainPage as PrincipalPage)?.NavegarA(new TareoDetallePage(proyecto));*/
            await Application.Current.MainPage.Navigation.PopModalAsync();

        }

        private async Task EditarTareo()
        {
            if (string.IsNullOrWhiteSpace(DeAct) || string.IsNullOrWhiteSpace(CoProy))
            {
                Resultado = "Por favor Ingresar la actividad";
                OnPropertyChanged(nameof(Resultado));
                return;
            }
            var Actualizado = await _db.UpdateTareosAsync(new TareoModel
            {
                Id = Id, // Asegúrate de que Id tenga un valor válido
                CoProy = CoProy,
                DeAct = DeAct,
                Fecha = Fecha,
                horaIni = horaIni,
                horaFin = horaFin
            });
            Resultado = Actualizado > 0 ? "Tareo actualizado correctamente" : "No se pudo actualizar el proyecto";
            OnPropertyChanged(nameof(Resultado));
            (Application.Current.MainPage as PrincipalPage)?.NavegarA(new TareoPage());
        }

        private async Task EliminarTareo()
        {
            if (Id!=0)
            {
                Mensaje = "Seleccionar la actividad";
                OnPropertyChanged(nameof(Mensaje));
                return;
            }
            bool confirmacion = await Application.Current.MainPage.DisplayAlert(
            "Confirmación",
            "¿Estás seguro que deseas continuar?",
            "Sí",
            "No");

            if (confirmacion)
            {
                var resultado = await _db.EliminarTareo(Id);
                Mensaje = resultado > 0 ? "Proyecto eliminado correctamente" : "No se pudo eliminar el proyecto";
                OnPropertyChanged(nameof(Mensaje));
                (Application.Current.MainPage as PrincipalPage)?.NavegarA(new TareoPage());
            }
        }



        private async Task BuscarTareoXproyectoAsync()
        {
            var listar = await _db.ServiciolistarTareoXproyecto(ProyectoBusqueda);
            Tareos.Clear();
            int i = 1;
            foreach (var p in listar)
            {
                p.Item = i++;
                Tareos.Add(p);
            }
        }



        private async Task Filtrar()
        {
            if (string.IsNullOrWhiteSpace(ProyectoBusqueda))
            {
                await BuscarTareoXproyectoAsync();
            }
            else
            {
                var filtrados = await _db.ServiciolistarTareoXproyecto(ProyectoBusqueda);
                ActualizarLista(filtrados);
            }
        }

        private void ActualizarLista(List<TareoModel> lista)
        {
            Tareos.Clear();
            foreach (var p in lista)
                Tareos.Add(p);
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
