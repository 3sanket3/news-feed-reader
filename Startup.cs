using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using news_feed_reader.Models;

namespace news_feed_reader
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<NewsFeedContext>(options =>
            {
                //Environment.SetEnvironmentVariable("EnvironmentName", _hostingEnvironment.EnvironmentName.ToLower() == "development" ? "local" : _hostingEnvironment.EnvironmentName);
                options.UseSqlServer(Configuration.GetConnectionString("NewsFeedDatabase"));
            });
            services.AddMvc();

            services.AddSingleton<IConfiguration>(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true,
                    ReactHotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });

           InitializeAsync(app.ApplicationServices).Wait();
            
        }


        public static async Task InitializeAsync(IServiceProvider service)
        {

            using (var serviceScope = service.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {

                var scopeServiceProvider = serviceScope.ServiceProvider;
                var db = scopeServiceProvider.GetService<NewsFeedContext>();

                db.Database.Migrate();
                await InsertSeedData(db);
            }
        }

        private static async Task InsertSeedData(NewsFeedContext context)
        {
            if (context.NewsFeedProviders.Any())
                return;

            List<NewsFeedProvider> lstNewsFeedProviders = new List<NewsFeedProvider>()
            {
                new NewsFeedProvider()
                {
                    Name="ABC News",
                    RssFeedURL="https://abcnews.go.com/abcnews/topstories"
                },
                new NewsFeedProvider()
                {
                    Name="BBC News",
                    RssFeedURL="http://feeds.bbci.co.uk/news/world/rss.xml"
                },
                new NewsFeedProvider()
                {
                    Name="CNN",
                    RssFeedURL="http://rss.cnn.com/rss/edition.rss"

                    },
                    new NewsFeedProvider()
                    {
                        Name="Times of India",
                        RssFeedURL="https://timesofindia.indiatimes.com/rssfeedstopstories.cms"
                    },
            };
            context.AddRange(lstNewsFeedProviders);
            await context.SaveChangesAsync();
        }
        
    }
}
