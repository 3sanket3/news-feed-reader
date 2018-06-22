using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using news_feed_reader.Models;

namespace news_feed_reader.Controllers
{
    [Route("api/[controller]")]
    public class NewsController : Controller
    {
        [HttpGet("[action]")]
        public object Get()
        {
            WebClient wclient = new WebClient();
            
            NewsFeedContext db = new NewsFeedContext();

            List<NewsItem> allNewsItems = new List<NewsItem>();

            foreach (var provider in db.NewsFeedProviders)
            {
                string RSSData = wclient.DownloadString(provider.RssFeedURL);

                XDocument xml = XDocument.Parse(RSSData);
                XNamespace media = XNamespace.Get("http://search.yahoo.com/mrss/");

                try
                {
                    var RSSFeedData = (from x in xml.Descendants("item")
                                       select new NewsItem
                                       {
                                           ThumbnailURL = x.Element(media + "thumbnail") != null ? x.Element(media + "thumbnail").Attribute("url").Value : "http://via.placeholder.com/100x100",
                                           Title = ((string)x.Element("title")),
                                           Link = ((string)x.Element("link")),
                                           Description = ((string)x.Element("description")),
                                           PubDate = ((DateTime)x.Element("pubDate")),
                                           Category = ((string)x.Element("category")),
                                           Provider = provider.Name
                                       });
                    allNewsItems.AddRange(RSSFeedData);
                }catch(Exception e)
                {
                    //It should notify the developer that some of the rss feeds parsing is failing. 

                    // Handeling the specific provider in try catch so that, if our parsing fail for one of the provider
                    // it should not affect the whole endpoint and will continue respond the data of otehr providers
                }
            }
            return allNewsItems.OrderByDescending(news => news.PubDate);
            
        }
    }
}