using AppRivera.Models;
using AppRivera.ViewModels;

namespace AppRivera.Views;

public partial class TareoPage : ContentPage
{
	public TareoPage()
	{
		InitializeComponent();
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is ProyectoViewModel vm)
        {
            if (vm.CargarComandInterfaz.CanExecute(null))
                vm.CargarComandInterfaz.Execute(null);
        }
    }
    private void btnNuevo_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ProyectoNuevoPage());
    }

    private void lstProyectos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is ProyectoModel proyecto)// 
        {
            // Navigate to the detail page with the selected project
            Navigation.PushAsync(new TareoDetallePage(proyecto));
        }
        // Deselect the item after selection
        ((ListView)sender).SelectedItem = null;
    }
}