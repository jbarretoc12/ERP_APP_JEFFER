namespace AppRivera.Views;

public partial class MenuPage : ContentPage
{
	public MenuPage()
	{
		InitializeComponent();
        lblcoUsu.Text = Preferences.Get("coUsu", "");
    }

    void IrInicio(Object sender, EventArgs e)
    {
        (Application.Current.MainPage as PrincipalPage)?.NavegarA(new InicioPage());
    }
    void IrProyectos(Object sender, EventArgs e)
    {
        (Application.Current.MainPage as PrincipalPage)?.NavegarA(new ProyectoPage());
    }
    private async void btnCerrarSesion_Clicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Cerrar sesi�n", "�Est�s seguro que deseas cerrar sesi�n?", "S�", "No");
        if (confirm)
        {
            Preferences.Clear(); // Borra datos de sesi�n o login
            Application.Current.MainPage = new LoginPage();
        }
    }

    private async void btnInicio_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new InicioPage());
    }
}