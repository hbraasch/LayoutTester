namespace LayoutTester;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new NavigationPage(new StartupPage(new StartupPageModel()));
	}
}
