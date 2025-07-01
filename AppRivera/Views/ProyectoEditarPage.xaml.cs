using AppRivera.Models;
using AppRivera.ViewModels;

namespace AppRivera.Views;

public partial class ProyectoEditarPage : ContentPage
{
    public ProyectoEditarPage(ProyectoModel proyecto)
    {
        InitializeComponent();
        /*txtCoProy.Text = proyecto.CoProy;
        txtCoProvCli.Text = proyecto.DeProy;*/
        BindingContext = new ProyectoViewModel
        {
            CoProy = proyecto.CoProy,
            DeProy = proyecto.DeProy
        };
    }
}