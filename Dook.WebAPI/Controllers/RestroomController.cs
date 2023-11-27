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
    public class RestroomController : ControllerBase
    {
        public static List<Restroom> RestroomList { get; } = new List<Restroom>();

        [HttpGet]
        public IEnumerable<Restroom> Get()
        {
            return RestroomList;
        }

        [HttpGet("{id}")]
        public Restroom Get(int id)
        {
            return RestroomList.FirstOrDefault(c => c.Id == id);
        }

        [HttpGet("GetList/{restId}")]
        public IEnumerable<Restroom> GetList(int restId)
        {
            return RestroomList.Where(rest => rest.RestroomId == restId).ToList();
        }

        [HttpPost]
        public void Post([FromBody] Restroom value)
        {
            RestroomList.Add(value);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Restroom value)
        {
            var restroom = RestroomList.FirstOrDefault(c => c.Id == id);
            if (restroom == null)
                return;

            restroom = value;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var restroom = RestroomList.FirstOrDefault(c => c.Id == id);
            if (restroom == null)
                return;

            RestroomList.Remove(restroom);
        }
    }
}