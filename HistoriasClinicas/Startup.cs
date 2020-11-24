using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HistoriasClinicas2.Data;
using HistoriasClinicas2.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HistoriasClinicas2
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
            services.AddControllersWithViews();

            if (Configuration["Contexto"] == "SQL")
            {
                services.AddDbContext<EFContext>(options => options.UseSqlServer(Configuration.GetConnectionString("NameDatabase")));
            }
            else
            {
                services.AddDbContext<EFContext>(options => options.UseInMemoryDatabase(databaseName: "DB-CS-CURSO-E-MEM"));
            }

            services.AddIdentity<Usuario, Rol>().AddEntityFrameworkStores<EFContext>();
            services.Configure<IdentityOptions>(opciones =>
            {
                opciones.Password.RequireLowercase = false;
                opciones.Password.RequireNonAlphanumeric = false;
                opciones.Password.RequireUppercase = false;
                opciones.Password.RequireDigit = false;
                opciones.Password.RequiredLength = 6;

                opciones.SignIn.RequireConfirmedEmail = false;
                opciones.SignIn.RequireConfirmedPhoneNumber = false;
            });

            services.AddTransient<IInitializationService, InitializationService>();

            services.PostConfigure<Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme,
                opciones =>
                {
                    opciones.LoginPath = "/Accounts/IniciarSesion";
                    opciones.AccessDeniedPath = "/Accounts/AccesoDenegado";
                }
                );

            services.AddControllersWithViews();
        }


    


    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, EFContext _EFcontext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            _EFcontext.Database.EnsureCreated();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var identityDbInitialize = scope.ServiceProvider.GetService<IInitializationService>();
                identityDbInitialize.Seed();
            }
        
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
