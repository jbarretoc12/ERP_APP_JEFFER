using System.Threading.Tasks;

namespace AppRivera.Views;

public partial class InicioPage : ContentPage
{
	public InicioPage()
	{
		InitializeComponent();
        

    }

    private  async void btnPruebaToken_Clicked(object sender, EventArgs e)
    {
        lblToken.Text = await SecureStorage.GetAsync("auth_token");
    }

}