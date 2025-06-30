using AppRivera.Models;
using AppRivera.ViewModels;

namespace AppRivera.Views;

public partial class TareoDetallePage : ContentPage
{
    ProyectoModel proyecto;
    public TareoDetallePage(ProyectoModel _proyecto)
	{
		InitializeComponent();
        proyecto = _proyecto;
        //BindingContext = new TareoViewModel(modelo); // pasar al ViewModel
        lblcoProy.Text = proyecto.CoProy;
        lbldeProy.Text = proyecto.DeProy;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is TareoViewModel vm)
        {
            if (vm.BuscarProyectosCommandInterfaz.CanExecute(null))
                vm.BuscarProyectosCommandInterfaz.Execute(null);
        }
    }

    private async void btnNuevoTareo_Clicked(object sender, EventArgs e)
    {
        var proy = new ProyectoModel
        {
            CoProy = lblcoProy.Text,
            DeProy = lbldeProy.Text
        };
        await Navigation.PushModalAsync(new TareoDetalleNuevoPage(proy));
    }
}