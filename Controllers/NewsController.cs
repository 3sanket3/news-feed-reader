using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using news_feed_reader.Models;
using news_feed_reader.Services;

namespace news_feed_reader.Controllers
{
    [Route("api/[controller]")]
    public class NewsController : Controller
    {
        [HttpGet("[action]")]
        public object Get(bool? onlySubscribed, int? providerId)
        {
            NewsFeedService service = new NewsFeedService();
            return service.getNewsFeeds(onlySubscribed,providerId);
        }

    }
}