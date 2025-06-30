using AppRivera.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using AppRivera.Models;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;

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
        public ICommand RegisterCommand { get; }
        public IAsyncRelayCommand LogOutCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new AsyncRelayCommand(Login);
            RegisterCommand = new Command(NavigateToRegister);
            LogOutCommand=new AsyncRelayCommand(LogOut);
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
                        //Application.Current.MainPage = new AppShell();
                        //await Shell.Current.GoToAsync("//PrincipalPage");
                        /* //para usar el token en otro lugar
                            var token = await SecureStorage.GetAsync("auth_token");
                            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                         */
                    }
                    else
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
        }

        private async void NavigateToRegister()
        {
            // Lógica para navegar a la página de registro
            //MessagingCenter.Send(this, "NavigateToRegister");
            //await Shell.Current.GoToAsync("RegisterPage");
            //await Shell.Current.GoToAsync(nameof(RegisterPage));
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());

        }

        private async Task LogOut()
        {
            try
            {
                bool confirm = await Application.Current.MainPage.DisplayAlert("Cerrar sesión", "¿Estás seguro que deseas cerrar sesión?", "Sí", "No");
                if (confirm)
                {
                    // Elimina el token del SecureStorage
                    SecureStorage.Remove("auth_token");

                    Preferences.Clear(); // Borra datos de sesión o login
                    Application.Current.MainPage = new LoginPage();
                }
                

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
