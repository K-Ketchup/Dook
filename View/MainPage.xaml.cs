namespace Dook;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using System.Diagnostics;
using Map = Microsoft.Maui.Controls.Maps.Map;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
        InitializeComponent();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Debug.WriteLine("Bruh");
    }
}


