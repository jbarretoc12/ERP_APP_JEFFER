
using AppRivera.Models;
using AppRivera.Services;
using AppRivera.ViewModels;
using Microsoft.Maui.Storage;
using System.Threading.Tasks;

namespace AppRivera.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
        InitializeComponent();
        BindingContext = new LoginViewModel();
    }
}