using Microsoft.AspNetCore.Mvc;
using Dook.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dook.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        public static List<Review> ReviewList { get; } = new List<Review>();

        [HttpGet]
        public IEnumerable<Review> Get()
        {
            return ReviewList;
        }

        [HttpGet("{id}")]
        public Review Get(int id)
        {
            var review = ReviewList.FirstOrDefault(c => c.Id == id);
            if (review == null)
                return null;
            return review;
        }

        [HttpGet("GetList/{restId}")]
        public IEnumerable<Review> GetList(int restId)
        {
            var reviews = ReviewList.Where(rest => rest.RestroomId == restId).ToList();
            if (reviews.Count == 0)
            {
                return new List<Review>();
            }
            return reviews;
        }

        [HttpPost]
        public void Post([FromBody] Review value)
        {
            ReviewList.Add(value);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Review value)
        {
            var review = ReviewList.FirstOrDefault(c => c.Id == id);
            if (review == null)
                return;

            review = value;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var review = ReviewList.FirstOrDefault(c => c.Id == id);
            if (review == null)
                return;

            ReviewList.Remove(review);
        }
    }
}