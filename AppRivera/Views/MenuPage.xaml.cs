using AppRivera.ViewModels;

namespace AppRivera.Views;

public partial class MenuPage : ContentPage
{
	public MenuPage()
	{
		InitializeComponent();
        BindingContext = new LoginViewModel();
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


}