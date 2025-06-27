using AppRivera.Models;

namespace AppRivera.Views;

public partial class TareoDetalleNuevoPage : ContentPage
{
    public TareoDetalleNuevoPage(ProyectoModel proyecto)
	{
		InitializeComponent();
        txtCoProy.Text = proyecto.CoProy;
        txtDeProy.Text = proyecto.DeProy;
    }

    private async void btnCerrarNuevo_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}