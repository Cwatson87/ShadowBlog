using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShadowBlog.Services.Interfaces;
using ShadowBlog.Services;
using ShadowBlog.Data;
using ShadowBlog.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;
using System.IO;

namespace ShadowBlog
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                   ConnectionService.GetConnectionString(Configuration)));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddIdentity<BlogUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultUI()
               .AddDefaultTokenProviders();
             
            services.AddControllersWithViews();

            //Register the DataService...
            services.AddTransient<DataService>();

            //Register the BasicImageService as the concrete class for the IImageService interface
            services.AddScoped<IImageService, BasicImageService>();
            services.AddScoped<ISlugService, BasicSlugService>();
            services.AddScoped<SearchService>();
            services.AddScoped<IEmailSender, BasicEmailService>();

            //Adding Swagger as a service
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "BlogApi",
                    Description = "This is an open API with no security",
                    Contact = new OpenApiContact
                    {
                        Name = "Cameron Watson",
                        Email = "cwatsdev@gmail.com",
                        Url = new Uri("https://camsblog.herokuapp.com/")
                    }              
                });

                //Construct the XML comment path
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShadowBlogAPI");
                c.RoutePrefix = "";
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //Custom route goes first...
                //Route definitions need to go from most specific to most generalized
                endpoints.MapControllerRoute(
                    name: "slugRoute",
                    pattern: "CameronsBlog/PostDetails/{slug}",
                    defaults: new { controller = "BlogPosts", action = "Details" }); 

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
