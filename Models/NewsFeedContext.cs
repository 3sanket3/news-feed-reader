using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace news_feed_reader.Models
{
    public class NewsFeedContext : DbContext
    {
        public DbSet<NewsFeedProvider> NewsFeedProviders { get; set; }

        public NewsFeedContext(DbContextOptions options) : base(options) { }
        
        public NewsFeedContext() { }

        public static IConfiguration Configuration { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

                Configuration = builder.Build();
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("NewsFeedDatabase"));
            
        }



    }

    
}
