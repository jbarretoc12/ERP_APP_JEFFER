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
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _correo;
        private string _clave;

        public string Correo
        {
            get => _correo;
            set { _correo = value; OnPropertyChanged(); }
        }

        public string Clave
        {
            get => _clave;
            set { _clave = value; OnPropertyChanged(); }
        }

        public Command LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(async () => await Login());
            RegisterCommand = new Command(NavigateToRegister);
        }

        //private async Task Login()
        //{
        //    var apiService = new ApiService();
        //    var response = await apiService.LoginAsync(Correo, Clave);
        //    // Manejar la respuesta
        //    if (response.Success)
        //    {
        //        // Navegar a la página principal
        //        await Shell.Current.GoToAsync("//PrincipalPage");
        //    }
        //    else
        //    {
        //        // Mostrar mensaje de error
        //        //await Application.Current.MainPage.DisplayAlert("Error", response.Message, "OK");
        //        await Shell.Current.DisplayAlert("Error", response.Message, "OK");
        //    }
        //}
        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set { _isBusy = value; OnPropertyChanged(); }
        }


        private async Task Login()
        {
            //if (string.IsNullOrWhiteSpace(Correo) || string.IsNullOrWhiteSpace(Clave))
            //{
            //    await Shell.Current.DisplayAlert("Advertencia", "Ingrese correo y clave", "OK");
            //    return;
            //}

            //IsBusy = true;
            //try
            //{
            //    var apiService = new ApiService();
            //    var response = await apiService.LoginAsync(Correo, Clave);
            //    if (response.Success)
            //    {
            //        Preferences.Set("Correo", Correo); // guardar sesión
            //        await Shell.Current.GoToAsync("//PrincipalPage");
            //    }
            //    else
            //    {
            //        //await Shell.Current.DisplayAlert("Error", response.Message, "OK");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    await Shell.Current.DisplayAlert("Error", $"Error inesperado: {ex.Message}", "OK");
            //}
            //finally
            //{
            //    IsBusy = false;
            //}
        }


        private async void NavigateToRegister()
        {
            // Lógica para navegar a la página de registro
            //MessagingCenter.Send(this, "NavigateToRegister");
            //await Shell.Current.GoToAsync("RegisterPage");
            //await Shell.Current.GoToAsync(nameof(RegisterPage));
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
