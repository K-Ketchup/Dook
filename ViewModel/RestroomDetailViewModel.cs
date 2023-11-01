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
    public class RestroomDetailViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Review> Review { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand AddCommand { get; }
        public AsyncCommand<Review> RemoveCommand { get; }

        public RestroomDetailViewModel()
        {
            Title = "My Review";

            Review = new ObservableRangeCollection<Review>();

            RefreshCommand = new AsyncCommand(RefreshAsync);
            AddCommand = new AsyncCommand(AddAsync);
            RemoveCommand = new AsyncCommand<Review>(RemoveAsync);
        }

        async Task AddAsync()
        {
            var username = await App.Current.MainPage.DisplayPromptAsync("Username", "Username for review");
            var stars = await App.Current.MainPage.DisplayPromptAsync("Stars", "Stars for review", maxLength: 1, keyboard: Keyboard.Numeric);
            var text = await App.Current.MainPage.DisplayPromptAsync("Text", "Add text", maxLength: 50);
            if (username == null || stars == null || text == null) { return; }
            await ReviewService.AddReviewAsync(username, Double.Parse(stars), text);
            await RefreshAsync();
        }
        async Task RemoveAsync(Review review)
        {
            await ReviewService.RemoveReviewAsync(review.Id);
            await RefreshAsync();
        }

        async Task RefreshAsync()
        {
            IsBusy = true;
            await Task.Delay(2000);
            Review.Clear();
            var reviews = await ReviewService.GetReviewAsync();
            Review.AddRange(reviews);
            IsBusy = false;
        }
    }
}
