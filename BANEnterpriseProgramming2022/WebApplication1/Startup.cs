using BusinessLogic.Services;
using DataAccess.Context;
using DataAccess.Repositories;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace WebApplication1
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
            services.AddDbContext<ShoppingCartContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
           
            services.AddDefaultIdentity<CustomUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ShoppingCartContext>();
           
            services.AddControllersWithViews();
            services.AddRazorPages();


            //to inform the startup (aka the injector class) about the service classes and the client classes
            //to inject and therefore initialize (e.g. ItemsRepository, ItemsServicess)
            /* Singleton: IoC container will create and share a single instance of a service throughout the application's lifetime.
             *note: if there are 50 users to browse the websit at the same time and all of them are using ItemsRepository at the same time
             *      only 1 ItemsRepository instance will be created
             *    note: if there are 50 users doing checkout at the same time the application will create 1 instance
             *    
               Transient: The IoC container will create a new instance of the specified service type every time you ask for it.
            note: if during the checkout the method ask for ItemsReposiotry 5x then 5 instances will be created;
            note: if there are 50 users doing checkout at the same time the application will create 50 instances x 5 calls =250

               Scoped: IoC container will create an instance of the specified service type once per request and will be shared in a single request.
            note: if during the chekout method in the ItemsService, you ask for the ItemsRepository 5x, only 1 instance will be created for me
            note: if there are 50 users doing checkout at the same time the application will create 50 instances x 1 = 50
            */

            FileInfo fi = new FileInfo(@"C:\Users\attar\source\repos\BANEP2022\BANEnterpriseProgramming2022\WebApplication1\Data\Categories.txt");

            services.AddScoped<ItemsRepository>();
            services.AddScoped<ItemsServices>();
           
            //services.AddScoped<ICategoriesRepository, CategoriesFileRepository>(provider =>
              // new CategoriesFileRepository(fi));

            //when you use interfaces to indicate to the injector class what type of object needs
            //to be initialized, you openning for more variations of the implementations you made
            //thus scaling up without a major rework of your architecture
            services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            services.AddScoped<CategoriesServices>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
