using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace news_feed_reader.Models
{
    public class NewsFeedProvider
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        public string WebsiteURL { get; set; }
        public string LogoURL { get; set; }
        public string RssFeedURL { get; set; }
        public bool isSubscribed { get; set; }

    }
}
