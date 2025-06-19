using AppRivera.Views;
using Microsoft.Maui.Controls;

namespace AppRivera
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("LoginPage", typeof(LoginPage));
            Routing.RegisterRoute("RegisterPage", typeof(RegisterPage));
            Routing.RegisterRoute("PrincipalPage", typeof(PrincipalPage));
            Routing.RegisterRoute("ProyectoPage", typeof(ProyectoPage));
            Routing.RegisterRoute("ProyectoNuevoPage", typeof(ProyectoNuevoPage));
            Routing.RegisterRoute("TareoPage", typeof(TareoPage));
            Routing.RegisterRoute("MenuPage", typeof(MenuPage));
            Routing.RegisterRoute("MenuPage", typeof(MenuPage));
        }
    }
}
