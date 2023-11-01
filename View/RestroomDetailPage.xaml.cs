using Dook.Services;
using Dook.Shared.Models;
using Dook.ViewModel;
using System.Windows.Input;

namespace Dook.View;

[QueryProperty(nameof(RestroomId), nameof(RestroomId))]
public partial class RestroomDetailPage : ContentPage
{
	public string RestroomId { get; set; }
	public Restroom restroom { get; set; }
	public RestroomDetailPage()
	{
		InitializeComponent();

        var vm = (InternetRestroomDetailViewModel)this.BindingContext;
        ToolbarItem item = new ToolbarItem
		{
			Text = "Add"
		};
		item.Clicked += async (s, args) =>
        {
            if (vm.AddCommand.CanExecute(restroom.Id.ToString()))
                await vm.AddCommand.ExecuteAsync(restroom.Id.ToString());
        };
        this.ToolbarItems.Add(item);

		ICommand refreshCommand = new Command(() =>
		{
			if (vm.RefreshCommand.CanExecute(restroom.Id.ToString()))
				vm.RefreshCommand.ExecuteAsync(restroom.Id.ToString());
			
		});
		ReviewList.RefreshCommand = refreshCommand;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		int.TryParse(RestroomId, out var result);

		restroom = await InternetRestroomService.GetSingularPinAsync(result);
		BindingContext = restroom;
	}

  //  private async void Button_Clicked(object sender, EventArgs e)
  //  {
		//await Shell.Current.GoToAsync("..");
  //  }
}