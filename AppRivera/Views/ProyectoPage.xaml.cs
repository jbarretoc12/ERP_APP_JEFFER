using AppRivera.Services;
using AppRivera.ViewModels;

namespace AppRivera.Views;

public partial class ProyectoPage : ContentPage
{
    ProyectoViewModel _viewModel;
    public ProyectoPage()
	{
		InitializeComponent();
        var databaseService = new DatabaseService();
        _viewModel = new ProyectoViewModel(databaseService);
        BindingContext = _viewModel;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ = _viewModel.ListarProyectos();
    }

    private void btnNuevo_Clicked(object sender, EventArgs e)
    {
        //(Application.Current.MainPage as PrincipalPage)?.NavegarA(new ProyectoNuevoPage());
        //Application.Current.MainPage = new NavigationPage(new ProyectoNuevoPage());
        Navigation.PushAsync(new ProyectoNuevoPage());
    }
}