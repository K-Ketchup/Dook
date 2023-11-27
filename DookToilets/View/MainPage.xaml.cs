namespace DookToilets;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using System.Diagnostics;
using Map = Microsoft.Maui.Controls.Maps.Map;
using DookToilets.ViewModel;
using DookToilets.Model;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        MoveMapLocation();
    }

    private void GoToLocation_Button(object sender, EventArgs e)
    {
        MoveMapLocation();
    }

    private void OnMapClicked(object sender, MapClickedEventArgs e)
    {
        var vm = (MainViewModel)this.BindingContext;
        if (vm.AddCommand.CanExecute(e.Location))
            vm.AddCommand.ExecuteAsync(e.Location);
    }

    private void RefreshButton_Clicked(object sender, EventArgs e)
    {
        var vm = (MainViewModel)this.BindingContext;
        vm.RefreshCommand.ExecuteAsync();
    }

    private void MoveMapLocation()
    {
        //Function to avoid boilerplate code
        MapSpan mapSpan = new MapSpan(MainViewModel.GetLocation(), 0.01, 0.01);
        mainmap.MoveToRegion(mapSpan);
    }
}