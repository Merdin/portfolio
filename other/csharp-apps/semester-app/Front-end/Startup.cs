using System;
using System.Globalization;
using System.Net;
using FrontEnd.Middlewares;
using FrontEnd.OAuth;
using FrontEnd.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace FrontEnd
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            // Implement custom HttpClient instances.
            services.AddHttpClient<ConstraintService>(client =>
                {
                    client.BaseAddress = new Uri(Configuration["EndPoints:BackEnd:ConstraintService"]);
                }
            );

            services.AddHttpClient<ModuleService>(client =>
                {
                    client.BaseAddress = new Uri(Configuration["EndPoints:BackEnd:ModuleService"]);
                }
            );

            services.AddHttpClient<StudyPlanService>(client =>
                {
                    client.BaseAddress = new Uri(Configuration["EndPoints:BackEnd:StudyPlanService"]);
                }
            );

            services.AddHttpClient<LearningUnitService>(client =>
                {
                    client.BaseAddress = new Uri(Configuration["EndPoints:BackEnd:LearningUnitService"]);
                }
            );

            services.AddHttpClient<UserService>(client =>
                {
                    client.BaseAddress = new Uri(Configuration["EndPoints:BackEnd:UserService"]);
                }
            );

            services.AddHttpClient<ModuleConstraintService>(client =>
                {
                    client.BaseAddress = new Uri(Configuration["EndPoints:BackEnd:ModuleConstraintService"]);
                }
            );

            services.AddHttpClient<LearningUnitModuleService>(client =>
                {
                    client.BaseAddress = new Uri(Configuration["EndPoints:BackEnd:LearningUnitModuleService"]);
                }
            );

            //LearningUnitModuleService
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
                    options =>
                    {
                        options.AccessDeniedPath = "/Home/Error";
                        options.LogoutPath = new PathString("/user/logout");
                        options.LoginPath = new PathString("/user/authenticate");
                    })
                .AddKeycloakAuthentication(options =>
                {
                    options.Authority = Configuration["Authentication:Keycloak:Authority"];
                    options.ClientId = Configuration["Authentication:Keycloak:ClientId"];
                    options.ClientSecret = Configuration["Authentication:Keycloak:ClientSecret"];

                    options.RequireHttpsMetadata = false;
                })
                .AddKeycloakTokenManagement();
            services.AddAuthorization();

            services.AddControllersWithViews();
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                options.KnownNetworks.Add(new IPNetwork(IPAddress.Parse("172.16.0.0"), 12));
                options.KnownNetworks.Add(new IPNetwork(IPAddress.Parse("192.168.32.0"), 20));
            });

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("nl"),
                    new CultureInfo("en-US")
                };

                options.DefaultRequestCulture = new RequestCulture(culture: "nl", uiCulture: "nl");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHttpsRedirection();
                app.UseForwardedHeaders();
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStaticFiles();

            var localeOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(localeOptions.Value);

            app.UseRouting();

            app.UseAuthentication();
            app.UseMiddlewares();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}