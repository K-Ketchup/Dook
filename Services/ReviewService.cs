using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dook.Model;

namespace Dook.Services
{
    public class ReviewService
    {
        List<Review> reviewList = new();

        public async Task<List<Review>> GetReviews()
        {
            if(reviewList.Count > 0)
                return reviewList;

            return reviewList;
        }
    }
}
