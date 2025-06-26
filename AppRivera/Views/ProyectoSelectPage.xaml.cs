using AppRivera.Models;
using AppRivera.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppRivera.Views;

public partial class ProyectoSelectPage : ContentPage
{
    ProyectoModel proyecto;
    public ProyectoSelectPage(ProyectoModel _proyecto)
	{
		InitializeComponent();
        proyecto = _proyecto;
        txtCoProy.Text = _proyecto.CoProy;
        lblCoProy.Text = _proyecto.CoProy;
		lblCoProvCli.Text = _proyecto.DeProy;
    }
    private void btnSelectProyecto_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ProyectoEditarPage(proyecto));
    }
}