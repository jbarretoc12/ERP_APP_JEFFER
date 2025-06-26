using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Input;
using AppRivera.Models;
using AppRivera.Services;
using AppRivera.Views;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace AppRivera.ViewModels
{
    public class ProyectoViewModel: INotifyPropertyChanged
    {

        public ObservableCollection<ProyectoModel> Proyectos { get; set; } = new();
        public ICommand CargarComandInterfaz { get;}
        public string CoProy { get; set; }
        public string DeProy { get; set; }

        #region guardar
        public ICommand GuardarComandInterfaz { get; }
        #endregion

        #region editar
        public ICommand EditarCommandInterfaz { get; }
        #endregion

        #region eliminar
        public ICommand EliminarCommandInterfaz { get; }
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

        public ICommand CargarProyectosCommandInterfaz { get; }
        #endregion


        private readonly DatabaseService _db;
        //public INavigation Navigation { get; set; }
        public string Resultado { get; set; }
        public ProyectoViewModel()
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "BDAppRivera.db3");
            _db = new DatabaseService(dbPath);

            CargarComandInterfaz = new Command(async () => await CargarProyectoAsync());

            GuardarComandInterfaz = new Command(async () => await Guardar());

            EditarCommandInterfaz=new Command(async () => await Editar());

            EliminarCommandInterfaz = new Command(async () => await Eliminar());

            CargarProyectosCommandInterfaz = new Command(async () => await CargarProyectoAsync());
        }



        private async Task CargarProyectoAsync()
        {
            var listar = await _db.ObtenerProyectosAsync();
            Proyectos.Clear();

            foreach (var p in listar)
            {
                Proyectos.Add(p);
            }
        }

        private async Task Guardar()
        {
            if (string.IsNullOrWhiteSpace(CoProy)) return;
            var NuevoProyecto=new ProyectoModel
            {
                CoProy = CoProy,
                DeProy = DeProy
            };

            await _db.InsertProyectoAsync(NuevoProyecto);
            (Application.Current.MainPage as PrincipalPage)?.NavegarA(new ProyectoPage());
            //await Navigation.PushAsync(new ProyectoPage());
        }

        private async Task Editar()
        {
            if(string.IsNullOrWhiteSpace(CoProy) || string.IsNullOrWhiteSpace(DeProy))
            {
                Resultado = "Por favor ingresar los datos";
                OnPropertyChanged(nameof(Resultado));
                return;
            }
            var Actualizado=await _db.UpdateProyectoAsync(new ProyectoModel
            {
                CoProy = CoProy,
                DeProy = DeProy
            });
            Resultado = Actualizado > 0 ? "Proyecto actualizado correctamente" : "No se pudo actualizar el proyecto";
            OnPropertyChanged(nameof(Resultado));
            (Application.Current.MainPage as PrincipalPage)?.NavegarA(new ProyectoPage());
        }

        private async Task Eliminar() {
            if (string.IsNullOrWhiteSpace(CoProy)){
                Mensaje = "Seleccionar un proyecto";
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
                var resultado = await _db.EliminarPorProyeto(CoProy);
                Mensaje = resultado > 0 ? "Proyecto eliminado correctamente" : "No se pudo eliminar el proyecto";
                OnPropertyChanged(nameof(Mensaje));
                (Application.Current.MainPage as PrincipalPage)?.NavegarA(new ProyectoPage());
            }
        }
        private async Task Filtrar()
        {
            if (string.IsNullOrWhiteSpace(TextoBusqueda))
            {
                await CargarProyectoAsync();
            }
            else
            {
                var filtrados = await _db.BuscarProyectoAsync(TextoBusqueda);
                ActualizarLista(filtrados);
            }
        }

        private void ActualizarLista(List<ProyectoModel> lista)
        {
            Proyectos.Clear();
            foreach (var p in lista)
                Proyectos.Add(p);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    }
}
