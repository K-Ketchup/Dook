using Dook.Services;

namespace Dook.View;

[QueryProperty(nameof(RestroomId), nameof(RestroomId))]
public partial class RestroomDetailPage : ContentPage
{
	public string RestroomId { get; set; }
	public RestroomDetailPage()
	{
		InitializeComponent();
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		int.TryParse(RestroomId, out var result);

		BindingContext = await InternetRestroomService.GetSingularPinAsync(result);
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync("..");
    }
}