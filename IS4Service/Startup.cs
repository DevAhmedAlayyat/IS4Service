using IdentityExpress.Identity;
using IdentityServer4;
using IdentityServer4.Validation;
using IS4Service.Data;
using IS4Service.IS4ExtendedClass;
using IS4Service.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace IS4Service
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
            string conStr = Configuration.GetConnectionString("DefaultConnection");
            string migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            services.AddControllersWithViews();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(conStr);
            });
            services.AddIdentity<ApplicationUser, IdentityExpressRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddIdentityExpressUserClaimsPrincipalFactory()
                .AddDefaultTokenProviders();


            services.AddAuthentication(options =>
            {
                if (options.DefaultAuthenticateScheme == null &&
                    options.DefaultScheme == IdentityServerConstants.DefaultCookieAuthenticationScheme)
                {
                    options.DefaultScheme = IdentityConstants.ApplicationScheme;
                }
            });
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddConfigurationStore(storeOptions =>
                {
                    storeOptions.ConfigureDbContext = b =>
                        b.UseSqlServer(conStr, npgSql => npgSql.MigrationsAssembly(migrationAssembly));
                })
                .AddOperationalStore(storeOptions =>
                {
                    storeOptions.ConfigureDbContext = b =>
                        b.UseSqlServer(conStr, npgSql => npgSql.MigrationsAssembly(migrationAssembly));
                })
                .AddAspNetIdentity<ApplicationUser>();

            //services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            app.UseStaticFiles();
        }
    }
}
