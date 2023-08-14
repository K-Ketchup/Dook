namespace Dook;
using Map = Microsoft.Maui.Controls.Maps.Map;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
        Map map = new Map();
        Content = map;
    }
}

