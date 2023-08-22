using Dook.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dook.Services;

namespace Dook.ViewModel
{
    public partial class MainViewModel : BaseViewModel
    {
        public ObservableCollection<Review> Reviews { get; } = new();
        public Command GetReviewsCommand { get; }
        ReviewService reviewService;

        public MainViewModel(ReviewService reviewService)
        {
            Title = "Review Finder";
            this.reviewService = reviewService;
            GetReviewsCommand = new Command(async () => await GetReviewsAsync());
        }
        async Task GetReviewsAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                var reviews = await reviewService.GetReviews();

                if (Reviews.Count != 0)
                    Reviews.Clear();

                foreach (var review in reviews)
                    Reviews.Add(review);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get reviews: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
