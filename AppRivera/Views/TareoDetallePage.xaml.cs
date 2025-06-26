using AppRivera.Models;

namespace AppRivera.Views;

public partial class TareoDetallePage : ContentPage
{
	public TareoDetallePage(ProyectoModel proyecto)
	{
		InitializeComponent();
        lblcoProy.Text = proyecto.CoProy;
        lbldeProy.Text = proyecto.DeProy;
    }
}