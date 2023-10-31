using Dook.View;

namespace Dook;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(RestroomDetailPage), typeof(RestroomDetailPage));
	}
}
