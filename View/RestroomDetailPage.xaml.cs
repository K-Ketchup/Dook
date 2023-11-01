using Dook.Services;
using Dook.Shared.Models;
using Dook.ViewModel;

namespace Dook.View;

[QueryProperty(nameof(RestroomId), nameof(RestroomId))]
public partial class RestroomDetailPage : ContentPage
{
	public string RestroomId { get; set; }
	public RestroomDetailPage()
	{
		InitializeComponent();

		ToolbarItem item = new ToolbarItem
		{
			Text = "Add"
		};
		item.Clicked += async (s, args) =>
        {
            var vm = (RestroomDetailViewModel)this.BindingContext;
            if (vm.AddCommand.CanExecute(RestroomId))
                await vm.AddCommand.ExecuteAsync(RestroomId);
        };

        this.ToolbarItems.Add(item);
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		int.TryParse(RestroomId, out var result);

		BindingContext = await InternetRestroomService.GetSingularPinAsync(result);
	}

  //  private async void Button_Clicked(object sender, EventArgs e)
  //  {
		//await Shell.Current.GoToAsync("..");
  //  }
}