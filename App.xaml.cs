namespace Dook;
using MonkeyCache.FileStore;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		Barrel.ApplicationId = AppInfo.PackageName;

		MainPage = new AppShell();
        UserAppTheme = AppTheme.Light;
    }
}
