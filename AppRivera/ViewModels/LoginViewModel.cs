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
using CommunityToolkit.Mvvm.Input;
using AppRivera.Models;
using System.Text.Json;

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

        public IAsyncRelayCommand LoginCommand { get; }

        //public Command LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        public LoginViewModel()
        {


            //LoginCommand = new Command(async () => await Login());
            LoginCommand = new AsyncRelayCommand(Login);
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
            try
            {
                var httpClient = new HttpClient();
                var loginData = new LoginRequest
                {
                    Correo = _correo, //"yy@dd.com",
                    Clave = _clave //"123"
                };

                var json = JsonSerializer.Serialize(loginData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("https://jbcprogramming.somee.com/api/Acceso/Login", content);
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<LoginResponse>(responseJson);

                    if (result.issuccess == true) { 

                        // Guardar el token de forma segura
                        await SecureStorage.SetAsync("auth_token", result.token);
                        //await Application.Current.MainPage.DisplayAlert("Éxito", "Inicio de sesión correcto", "OK");
                        Application.Current.MainPage = new PrincipalPage();
                        /*
                         //para usar el token en tro luygar
                            var token = await SecureStorage.GetAsync("auth_token");

                            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                         */                    

                    }else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Credenciales incorrectas", "OK");
                    }



                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "No se ecnotro el servicio", "OK");
                }

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
            }



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
