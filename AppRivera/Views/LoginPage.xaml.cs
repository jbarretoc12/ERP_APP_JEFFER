
using AppRivera.Models;
using AppRivera.Services;
using AppRivera.ViewModels;
using Microsoft.Maui.Storage;
using System.Threading.Tasks;

namespace AppRivera.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
        InitializeComponent();
        BindingContext = new LoginViewModel();
    }

    private void btnIniciar_Clicked(object sender, EventArgs e)
    {
       /* if (txtCorreo.Text == "admin" && txtClave.Text == "123")
        {
            //usar "SecureStorage" para datos mas sensibles como token, claves, etc. 

            Preferences.Get("EstaLogeago", true);//sesion para detectar si esta logueado
            Preferences.Set("coUsu", txtCorreo.Text);//sesion para mantener el usuario
            // Reemplazar la página principal por la NavigationPage(MainPage)
            //Application.Current.MainPage = new NavigationPage(new PrincipalPage());
            Application.Current.MainPage = new PrincipalPage();
        }
        else
        {
            DisplayAlert("Error", "Credenciales incorrectas.", "OK");
        }*/
    }
}