using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace news_feed_reader.Models
{
    public class NewsFeedContext : DbContext
    {
        public DbSet<NewsFeedProvider> NewsFeedProviders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=newsfeed_sanketpatel;Integrated Security=True");
        }
}
}
