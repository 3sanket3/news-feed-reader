using news_feed_reader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace news_feed_reader.Services
{
    public class NewsFeedService
    {

        internal object getNewsFeeds(bool? onlySubscribed, int? providerId)
        {
            WebClient wclient = new WebClient();

            NewsFeedContext db = new NewsFeedContext();
            List<NewsFeedProvider> lstProviders = new List<NewsFeedProvider>();
            if(providerId > 0)
            {
                //if onlySubscribed and provider id both present, provider id will be given preference
                lstProviders = db.NewsFeedProviders.Where(prov => prov.Id == providerId).ToList();
            }
            else if (onlySubscribed ==true)
            {
                lstProviders = db.NewsFeedProviders.Where(prov => prov.isSubscribed == true).ToList();
            }
            else
            {
                lstProviders = db.NewsFeedProviders.ToList();
            }

            List<NewsItem> allNewsItems = new List<NewsItem>();

            foreach (var provider in lstProviders)
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
                                           Provider = provider.Name,
                                           ProviderId = provider.Id

                                       });
                    allNewsItems.AddRange(RSSFeedData);
                }
                catch (Exception e)
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
