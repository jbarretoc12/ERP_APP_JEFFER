namespace AppRivera.Views;

public partial class ProyectoPage : ContentPage
{
	public ProyectoPage()
	{
		InitializeComponent();
	}

    private void btnInicio_Clicked(object sender, EventArgs e)
    {
        (Application.Current.MainPage as PrincipalPage)?.NavegarA(new InicioPage());
    }
}