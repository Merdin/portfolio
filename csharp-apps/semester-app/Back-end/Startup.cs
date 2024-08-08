using System.Net;
using System.Net.Mail;
using Backend.Data;
using Backend.Jwt;
using Backend.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Backend
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
            services.ConfigureJwtAuthentication(options =>
            {
                options.Audience = Configuration["Jwt:Audience"];
                options.Authority = Configuration["Jwt:Issuer"];
                options.TokenValidationParameters.ValidIssuer = options.Authority;
                options.TokenValidationParameters.NameClaimType = "name";
            });
            services.ConfigureJwtAuthorization();

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(
                    JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build();
            });
            services.AddControllers();
            services.AddDbContextPool<DataContext>(
                options => options.EnableSensitiveDataLogging().UseNpgsql(Configuration.GetConnectionString("pgsql"))
            );
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddFluentEmail("noreply@studym8.nl").AddSmtpSender(
                new SmtpClient(Configuration["Smtp:Address"], int.Parse(Configuration["Smtp:Port"]))
                {
                    Credentials = new NetworkCredential(Configuration["Smtp:Username"], Configuration["Smtp:Password"])
                }
            );
            services.AddControllers().AddNewtonsoftJson(s =>
                s.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                this.ApplyMigrations(app);
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            SeedData.Initialize(context);
        }

        public void ApplyMigrations(IApplicationBuilder app)
        {
            IServiceScope scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            {
                scope.ServiceProvider.GetService<DataContext>().Database.Migrate();
            }
        }
    }
}