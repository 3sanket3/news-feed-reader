using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace news_feed_reader.Models
{
    public class NewsItem
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public DateTime PubDate { get; set; }
        public string Description { get; set; }
        public string ThumbnailURL { get; set; }
        public string Category { get; set; }
        public string Provider { get; set; }
    }
}
