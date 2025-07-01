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
        //if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
        //{
        //    var proyectoSeleccionado = e.CurrentSelection.FirstOrDefault() as ProyectoModel;

        //    if (proyectoSeleccionado != null)
        //    {
        //        Navigation.PushAsync(new ProyectoEditarPage(proyectoSeleccionado));
        //    }

        //    ((CollectionView)sender).SelectedItem = null;
        //}

        if (e.CurrentSelection.FirstOrDefault() is ProyectoModel seleccionado)
        {
            // Cambia el fondo del ítem seleccionado
            foreach (var item in collectionProyectos.ItemsSource)
            {
                if (item is ProyectoModel proyecto)
                    proyecto.IsSelected = proyecto == seleccionado;
            }
        }
    }

    private async void OnProyectoTapped(object sender, TappedEventArgs e)
    {
        if (sender is Grid grid && e.Parameter is ProyectoModel proyecto)
        {
            // Animación
            await grid.ScaleTo(0.97, 80, Easing.CubicOut);
            await grid.ScaleTo(1.0, 80, Easing.CubicIn);

            // Ir a la página de edición
            await Navigation.PushAsync(new ProyectoEditarPage(proyecto));
        }
    }

    private async void OnItemTapped(object sender, EventArgs e)
    {
        if (sender is Grid grid && grid.BindingContext is ProyectoModel proyecto)
        {
            // Animación
            await grid.ScaleTo(0.95, 100, Easing.CubicInOut);
            await grid.ScaleTo(1, 100, Easing.CubicInOut);

            // Ir a la página de edición
            await Navigation.PushAsync(new ProyectoEditarPage(proyecto));
        }
    }

}