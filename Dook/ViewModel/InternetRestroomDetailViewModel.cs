using Dook.Shared.Models;
using MvvmHelpers.Commands;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dook.Services;

namespace Dook.ViewModel
{
    public class InternetRestroomDetailViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Review> Review { get; set; }
        public String rID { get; set; }
        public AsyncCommand<String> RefreshCommand { get; }
        public AsyncCommand<String> AddCommand { get; }
        public AsyncCommand<Review> RemoveCommand { get; }

        public InternetRestroomDetailViewModel()
        {
            Title = "My Review";

            Review = new ObservableRangeCollection<Review>();

            RefreshCommand = new AsyncCommand<String>(RefreshAsync);
            AddCommand = new AsyncCommand<String>(AddAsync);
            RemoveCommand = new AsyncCommand<Review>(RemoveAsync);
        }

        async Task AddAsync(String restId)
        {
            var username = await App.Current.MainPage.DisplayPromptAsync("Username", "Username for review");
            var stars = await App.Current.MainPage.DisplayPromptAsync("Stars", "Stars for review", maxLength: 1, keyboard: Keyboard.Numeric);
            var text = await App.Current.MainPage.DisplayPromptAsync("Text", "Add text", maxLength: 50);
            rID = restId;
            if (username == null || stars == null || text == null) { return; }
            await InternetReviewService.AddReviewAsync(username, Double.Parse(stars), text, Int32.Parse(restId));
            await RefreshAsync(restId);
        }
        async Task RemoveAsync(Review review)
        {
            await InternetReviewService.RemoveReviewAsync(review.Id);
            await RefreshAsync(rID);
        }

        async Task RefreshAsync(String restId)
        {
            IsBusy = true;
            await Task.Delay(2000);
            Review.Clear();
            var reviews = await InternetReviewService.GetReviewAsync(Int32.Parse(restId));
            try
            {
                Review.AddRange(reviews);
            }
            catch(Exception ex) 
            {
                Debug.WriteLine(ex.Message);
            }
            IsBusy = false;
        }
    }
}
