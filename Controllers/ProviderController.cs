using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using news_feed_reader.Models;

namespace news_feed_reader.Controllers
{
    [Produces("application/json")]
    [Route("api/Provider")]
    public class ProviderController : Controller
    {
        [HttpGet("[action]")]
        public object Get()
        {
            NewsFeedContext db = new NewsFeedContext();

            return db.NewsFeedProviders;
        }

        [HttpPut("[action]")]
        public object Put(int id, bool subscribe)
        {
            NewsFeedContext db = new NewsFeedContext();
            NewsFeedProvider provider = db.NewsFeedProviders.Where(prov => prov.Id == id).Single();
            provider.isSubscribed = subscribe;
            db.SaveChanges();

            return db.NewsFeedProviders;
        }
    }
}