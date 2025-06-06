
using AppRivera.Models;
using AppRivera.Services;
using Microsoft.Maui.Storage;
using System.Threading.Tasks;

namespace AppRivera.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}
    private readonly AuthService _authService = new AuthService();
    private async void btnAcceder_Clicked(object sender, EventArgs e)
    {

        var token = await _authService.LoginAsync(UsernameEntry.Text, PasswordEntry.Text);

        if (!string.IsNullOrEmpty(token))
        {
            await SecureStorage.SetAsync("auth_token", token);
            Application.Current.MainPage = new PrincipalPage();
        }
        else
        {
            await DisplayAlert("Error", "Credenciales incorrectas.", "OK");
        }






       /* if (UsernameEntry.Text == "admin" && PasswordEntry.Text == "123")
        {
            //usar "SecureStorage" para datos mas sensibles como token, claves, etc. 

            Preferences.Get("EstaLogeago", true);//sesion para detectar si esta logueado
            Preferences.Set("coUsu",UsernameEntry.Text);//sesion para mantener el usuario
            // Reemplazar la página principal por la NavigationPage(MainPage)
            //Application.Current.MainPage = new NavigationPage(new PrincipalPage());
            Application.Current.MainPage = new PrincipalPage();
        }
        else
        {
            await DisplayAlert("Error", "Credenciales incorrectas.", "OK");
        }*/
    }
}