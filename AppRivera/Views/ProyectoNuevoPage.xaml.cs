using Microsoft.Maui.Controls;
using AppRivera.ViewModels;
using AppRivera.Services;

namespace AppRivera.Views;

public partial class ProyectoNuevoPage : ContentPage
{
    //ProyectoViewModel ViewModel;
	public ProyectoNuevoPage()
	{
		InitializeComponent();
       // BindingContext = new ProyectoViewModel(); //ViewModel;  
        var databaseService = new DatabaseService();
        BindingContext = new ProyectoViewModel(databaseService);
    }

    private void btnGrabar_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is ProyectoViewModel vm)
        {
            if (vm.NuevoProyectoCommand.CanExecute(null))
                vm.NuevoProyectoCommand.Execute(null);
        }

        (Application.Current.MainPage as PrincipalPage)?.NavegarA(new ProyectoPage());
    }
}