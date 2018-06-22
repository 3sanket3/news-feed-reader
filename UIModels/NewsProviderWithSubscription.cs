using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace news_feed_reader.UIModels
{
    public class NewsProviderWithSubscription
    {
        public string Name { get; set; }
        public bool isSubscribed { get; set; }
        public int ProviderId { get; set; }
    }
}
