using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using AppRivera.Models;
using AppRivera.Services;
namespace AppRivera.ViewModels
{
    public class ProyectoViewModel
    {

        private string _coProv;
        public string CoProv
        {
            get => _coProv;
            set
            {
                if (_coProv != value)
                {
                    _coProv = value;
                    OnPropertyChanged(nameof(CoProv));
                }
            }
        }

        private string _coProvCli;
        public string CoProvCli
        {
            get => _coProvCli;
            set
            {
                if (_coProvCli != value)
                {
                    _coProvCli = value;
                    OnPropertyChanged(nameof(CoProvCli));
                }
            }
        }

        private readonly DatabaseService _databaseService;
        public ObservableCollection<ProyectoModel> Proyectos { get; set; }
        public ICommand NuevoProyectoCommand { get; }
        public ICommand ListarProyectoCommand { get; }


        public ProyectoViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
             Proyectos = new ObservableCollection<ProyectoModel>();
            NuevoProyectoCommand = new Command(async () => await NuevoProyecto());
            ListarProyectoCommand = new Command(async () => await ListarProyectos());

        }
        public ProyectoViewModel()
        {

        }
        public async Task NuevoProyecto()
        {
            var nuevoProyecto = new ProyectoModel
            {
                CoProy = this.CoProv,
                CoProvCli = this.CoProvCli
            };
            await _databaseService.GetConnection().InsertAsync(nuevoProyecto);
            //Proyectos.Add(nuevoProyecto);
        }
        public async Task ListarProyectos()
        {
            Proyectos.Clear();
            var proyectos = await _databaseService.GetConnection().Table<ProyectoModel>().ToListAsync();
            Proyectos.Clear();
            foreach (var proyecto in proyectos)
            {
                Proyectos.Add(proyecto);
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
