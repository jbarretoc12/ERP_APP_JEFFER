using AppRivera.Models;
using AppRivera.Services;
using AppRivera.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppRivera.ViewModels
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        private string _nombre;
        private string _correo;
        private string _clave;

        public string Nombre
        {
            get => _nombre;
            set
            {
                _nombre = value; OnPropertyChanged();
            }
        }

        public string Correo
        {
            get => _correo;
            set
            {
                _correo = value; OnPropertyChanged();
            }
        }

        public string Clave
        {
            get => _clave;
            set
            {
                _clave = value; OnPropertyChanged();
            }
        }

        public ICommand RegisterCommand { get; }
        public ICommand GoToLoginCommand { get; }

        private readonly ApiService _apiService;

        public RegisterViewModel()
        {
            Console.WriteLine("===> Se creó RegisterViewModel");
            //RegisterCommand = new Command(OnRegister);
            //GoToLoginCommand = new Command(OnGoToLogin);
            _apiService = new ApiService();
            RegisterCommand = new Command(async () => await OnRegister());
            //RegisterCommand = new Command(() => OnRegister());

        }

        //private async void OnRegister()
        private async Task OnRegister()
        {
            var request = new User
            {
                Nombre = Nombre,
                Correo = Correo,
                Clave = Clave
            };

            bool result = await _apiService.RegisterAsync(request);

            if (result)
            {
                await Application.Current.MainPage.DisplayAlert("Éxito", "Registro exitoso", "OK");
                //await Shell.Current.GoToAsync(nameof(LoginPage));
                //await Shell.Current.GoToAsync("//LoginPage");
                //Application.Current.MainPage = new NavigationPage(new LoginPage());
                await Shell.Current.GoToAsync("//LoginPage");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No se pudo registrar", "OK");
            }

            //await Shell.Current.DisplayAlert("Registrado", "Cuenta creada correctamente", "OK");
        }

        /*private async void OnGoToLogin()
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }*/

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
