using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DependencyInjection = SingleSignOn.StartupServices.DependencyInjection;
using GlobalSettings = SingleSignOn.StartupServices.GlobalSettings;
using CachingStartup = SingleSignOn.StartupServices.Caching;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Threading.Tasks;
using SingleSignOn.Utils;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace SingleSignOn
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
            services.AddMvc(option => option.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Latest);
            new GlobalSettings.Startup().Start(Configuration);
            new DependencyInjection.Startup().Start(services);
            new CachingStartup.Startup().Start(services);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(AppSettings.SecretKey))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine("Invalid Token" + context.Exception.Message);
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = context =>
                        {
                            Console.WriteLine("Valid Token " + context.SecurityToken);
                            return Task.CompletedTask;
                        }
                    };

                });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Single Sign On - API",
                        Version = "v1",
                        Description = "Single Sign On Microservice",
                        Contact = new OpenApiContact
                        {
                            Name = "Otávio Larrosa",
                            Url = new Uri("https://github.com/otaviolarrosa/SingleSignOn")
                        }
                    });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/v1/swagger.json", $"Single Sign On Api");
                c.RoutePrefix = "swagger";
            });
            app.UseMvc();
            
        }
    }
}
