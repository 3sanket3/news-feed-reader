using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace news_feed_reader.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public int NewFeedProviderId { get; set; }
    }
}
