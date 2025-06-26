using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Maui.Networking;
using AppRivera.Models;

namespace AppRivera.Views;

public partial class InicioPage : ContentPage
{
	public InicioPage()
	{
		InitializeComponent();
        Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;

        // Comprobar al iniciar
        VerificarConexion();

    }
    private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
    {
        VerificarConexion();
    }
    private void VerificarConexion()
    {
        var acceso = Connectivity.Current.NetworkAccess;

        if (acceso == NetworkAccess.Internet)
        {
            // Conectado
            lblEstadoConexion.Text = "Conectado a Internet";
            lblEstadoConexion.TextColor = Colors.Green;
        }
        else
        {
            // No conectado
            lblEstadoConexion.Text = "Sin conexión";
            lblEstadoConexion.TextColor = Colors.Red;
        }
    }

    private  async void btnPruebaToken_Clicked(object sender, EventArgs e)
    {
        var token = await SecureStorage.GetAsync("auth_token");
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }


    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        (Application.Current.MainPage as PrincipalPage)?.NavegarA(new ProyectoPage());
    }

    private void TapGestureRecognizer_Tapped_1(object sender, TappedEventArgs e)
    {
        (Application.Current.MainPage as PrincipalPage)?.NavegarA(new TareoPage());
    }
}