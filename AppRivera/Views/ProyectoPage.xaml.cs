using AppRivera.Models;
using AppRivera.Services;
using AppRivera.ViewModels;
using System.Threading.Tasks;

namespace AppRivera.Views;

public partial class ProyectoPage : ContentPage
{
    public ProyectoPage()
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
    private void lstProyectos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is ProyectoModel proyecto)// 
        {
            // Navigate to the detail page with the selected project
            Navigation.PushAsync(new ProyectoSelectPage(proyecto));
        }
        // Deselect the item after selection
        ((ListView)sender).SelectedItem = null;
    }

    private void btnNuevo_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ProyectoNuevoPage());
    }

    private void btnEliminar_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("Base de datos", "Se eliminó correctamente", "OK");
    }

    private void collectionProyectos_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
        {
            var proyectoSeleccionado = e.CurrentSelection.FirstOrDefault() as ProyectoModel;

            if (proyectoSeleccionado != null)
            {
                Navigation.PushAsync(new ProyectoEditarPage(proyectoSeleccionado));
            }

            ((CollectionView)sender).SelectedItem = null;
        }
    }
}