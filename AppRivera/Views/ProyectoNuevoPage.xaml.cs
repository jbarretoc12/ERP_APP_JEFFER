using Microsoft.Maui.Controls;
using AppRivera.ViewModels;
using AppRivera.Services;

namespace AppRivera.Views;

public partial class ProyectoNuevoPage : ContentPage
{
	public ProyectoNuevoPage()
	{
		InitializeComponent();
    }
    private void btnGrabar_Clicked(object sender, EventArgs e)
    {
        (Application.Current.MainPage as PrincipalPage)?.NavegarA(new ProyectoPage());
    }
}