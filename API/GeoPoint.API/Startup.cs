using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using GeoPoint.Models.Data;
using GeoPoint.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using GeoPoint.Models.Repositories;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Mvc.Formatters;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Mvc.Versioning;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GeoPoint.API
{
    public class Startup
    {
        private IHostingEnvironment _env;
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            _env = env;

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddMvc(options => 
            {
                if (!_env.IsProduction())
                {
                    options.SslPort = 44343;
                }
                options.Filters.Add(new RequireHttpsAttribute());
                options.RespectBrowserAcceptHeader = true; //false by default
                options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<GeoPointAPIContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("GeoPointAPIContext")));
            services.AddScoped<IFriendsRepo, FriendsRepo>();
            services.AddScoped<IScoreRepo, ScoreRepo>();
            services.AddIdentity<GeoPointUser, IdentityRole>()
                 .AddEntityFrameworkStores<GeoPointAPIContext>()
                 .AddDefaultTokenProviders();
            services.AddAuthentication("GeoPointScheme")
                .AddCookie("GeoPointScheme", options =>
                {
                    options.Events =
                    new CookieAuthenticationEvents()
                    {
                        OnRedirectToLogin = (ctx) =>
                        {
                            if (ctx.Request.Path.StartsWithSegments("/api") &&
                        ctx.Response.StatusCode == 200) //redirect is 200
                        {
                            //doe geen redirect naar een loginpagina bij een api call
                            //maar geef een 401 - unauthorized
                            ctx.Response.StatusCode = 401;
                            }
                            return Task.CompletedTask;
                        },
                        OnRedirectToAccessDenied = (ctx) =>
                        {
                            if (ctx.Request.Path.StartsWithSegments("/api") &&
                ctx.Response.StatusCode == 200)
                            {

                                ctx.Response.StatusCode = 403; //uitvoering refused
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
            services.AddTransient<SeedIdentity>();
            services.AddCors(cfg =>
            {
                cfg.AddPolicy("GeoPoint", builder =>
                {
                    builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins("http://GeoPoint.be");
                });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.0", new Info
                {
                    Title = "GeoPoint.API",
                    Version = "v1.0"
                });
            });
            services.AddApiVersioning(cfg => {
                cfg.DefaultApiVersion = new ApiVersion(0, 1);
                cfg.AssumeDefaultVersionWhenUnspecified = true;
                cfg.ReportApiVersions = true;
                cfg.ApiVersionReader = new HeaderApiVersionReader("ver");
            });
            services.AddMemoryCache();
            services.Configure<IpRateLimitOptions>((options) =>
            {
                options.GeneralRules = new System.Collections.Generic.List<RateLimitRule>()
                {
                    new RateLimitRule() {
                    Endpoint = "*",
                    Limit=50,
                    Period ="5m"
                    }
                };
            });
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Tokens:Issuer"],
                        ValidAudience = Configuration["Tokens:Audience"],

                        IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))
                    };
                    options.SaveToken = false;
                    options.RequireHttpsMetadata = false;
                });
            services.AddSingleton<GeoPointAPIMongoDBContext>();
            services.AddTransient<SeedMongo>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, SeedIdentity seedIdentity, SeedMongo seedMongo)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<GeoPointAPIContext>();
                context.Database.Migrate();
            }
            app.UseCors(cfg =>
            {
                cfg.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
            });
            app.UseIpRateLimiting();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseRewriter(new RewriteOptions().AddRedirectToHttps(301, 44343));
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.RoutePrefix = "swagger"; //kan je dus aanpassen naar een ander uri
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "GeoPoint.API v1.0");
            });
            app.UseAuthentication();
            app.UseMvc();
            seedIdentity.SeedIdentityMobileAppsAPI().Wait();
            seedMongo.initDatabase(150);
        }
    }
}
