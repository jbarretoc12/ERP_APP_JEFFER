namespace AppRivera.Views;

public partial class PrincipalPage : FlyoutPage
{
	public PrincipalPage()
	{
		InitializeComponent();

	}
	public void NavegarA(ContentPage page)
	{
		Detail = new NavigationPage(page);
		IsPresented = false; //cerrar el menu
	}
}