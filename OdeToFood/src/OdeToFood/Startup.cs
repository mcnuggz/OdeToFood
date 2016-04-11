using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OdeToFood.Services;
using OdeToFood.Entities;
using Microsoft.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace OdeToFood
{
    public class Startup
    {
        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
        }
        public IConfiguration Configuration { get; set; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<OdeToFoodDbContext>(options => options.UseSqlServer(Configuration["database:connection"]));
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<OdeToFoodDbContext>();
            services.AddSingleton(provider => Configuration);
            services.AddSingleton<IGreeter, Greeter>();
            services.AddScoped<IRestaurantData, SqlRestaurantData>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment environment, IGreeter greeter)
        {
            app.UseIISPlatformHandler();

            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
               
            app.UseRuntimeInfoPage("/info");

            app.UseFileServer();

            app.UseIdentity();

            app.UseMvc(ConfigureRoutes);

            app.Run(async (context) =>
            {
                var greeting = greeter.GetGreeting();
                await context.Response.WriteAsync(greeting);
            });
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute("Default", 
                "{controller=Home}/{action=Index}/{id?}");
        }

        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
