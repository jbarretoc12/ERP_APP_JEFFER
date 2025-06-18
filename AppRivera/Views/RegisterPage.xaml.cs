using AppRivera.ViewModels;

namespace AppRivera.Views;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
        BindingContext = new RegisterViewModel();
    }
}