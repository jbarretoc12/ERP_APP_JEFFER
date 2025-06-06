using AppRivera.Views;

namespace AppRivera
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //verificar si hay sesion iniciada
            bool estaLogeago = Preferences.Get("EstaLogeago", false);


            if (estaLogeago == true)
            {
                MainPage = new NavigationPage(new PrincipalPage());
                //MainPage = new PrincipalPage();
                
            }
            else
            {
                MainPage = new LoginPage();
            }

            //MainPage = new PrincipalPage();
        }


       
    }
}
