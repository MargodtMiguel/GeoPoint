using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GeoPoint.Models.Data;
using GeoPoint.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using GeoPoint.Models.Repositories;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Versioning;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AspNetCore.Identity.Mongo;
using GeoPoint.API.Hubs;
using Microsoft.Extensions.Logging;
using AspNetCore.Identity.Mongo.Model;
using System;
using MongoDB.Driver;

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

        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddMvc(options => 
            {
                if (!_env.IsProduction())
                {
                    options.SslPort = 44343;
                }
                options.Filters.Add(new RequireHttpsAttribute());
                options.RespectBrowserAcceptHeader = true;
                options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            

            //Repos
            services.AddScoped<IScoreRepo, ScoreRepo>();

            //cookie auth
            services.AddAuthentication("GeoPointScheme")
                .AddCookie("GeoPointScheme", options =>
                {
                    options.Events = new CookieAuthenticationEvents()
                    {
                        OnRedirectToLogin = (ctx) =>
                        {
                            if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200) 
                            {
                                ctx.Response.StatusCode = 401;
                            }
                            return Task.CompletedTask;
                        },
                        OnRedirectToAccessDenied = (ctx) =>
                        {
                            if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                            {
                                ctx.Response.StatusCode = 403; //uitvoering refused
                            }
                            return Task.CompletedTask;
                        }
                    };
                    options.Cookie.MaxAge = new TimeSpan(0, 1, 0, 0, 0);
                });

            //Cross origin
            services.AddCors(options => options.AddPolicy("CorsPolicy",
              builder =>
              {
                  builder.AllowAnyMethod().AllowAnyHeader()
                         .WithOrigins("http://localhost:8080","https://margodtmiguel.github.io", "https://runeclaeys.github.io")
                         .AllowCredentials();
              }));

            //Swagger
            services.AddSwaggerDocumentation();

            //Versioning
            services.AddApiVersioning(cfg => {
                cfg.DefaultApiVersion = new ApiVersion(1, 0);
                cfg.AssumeDefaultVersionWhenUnspecified = true;
                cfg.ReportApiVersions = true;
                cfg.ApiVersionReader = new MediaTypeApiVersionReader();
            });

            //Cachinng
            services.AddMemoryCache();

            //Rate limiting
            services.Configure<IpRateLimitOptions>((options) =>
            {
                options.GeneralRules = new System.Collections.Generic.List<RateLimitRule>()
                {
                    new RateLimitRule() {
                    Endpoint = "*",
                    Limit=5000,
                    Period ="5m"
                    }
                };
            });
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();

            //Token Auth
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

            //MongoDB
            if (_env.IsDevelopment())
            {
                services.AddIdentityMongoDbProvider<GeoPointUser, MongoRole>(options =>
                {
                    string connectionString = "mongodb://localhost:27017/GeoPoint";

                    options.ConnectionString = connectionString;
                    options.UsersCollection = "Users";
                    options.RolesCollection = "Roles";

                });
            }
            else
            {
                services.AddIdentityMongoDbProvider<GeoPointUser, MongoRole>(options =>
                {
                    string connectionString = @"mongodb://geopoint:cVP4AZwCGVxRjaMEqdNHBTBFHWEa4z1M3Is2MK9APpKX5EeoOjpIlvrfz48IA6IofQbey922M8dsDyGS3ngtEA==@geopoint.documents.azure.com:10255/GeoPoint?ssl=true&replicaSet=globaldb";

                    options.ConnectionString = connectionString;
                    options.UsersCollection = "Users";
                    options.RolesCollection = "Roles";

                });
            }

           
            services.AddSingleton<GeoPointAPIMongoDBContext>();
            services.AddTransient<SeedMongo>();
            
            services.AddSignalR(o => o.EnableDetailedErrors = true);
        }



        public void Configure(IApplicationBuilder app, IHostingEnvironment env,SeedMongo seedMongo, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
                loggerFactory.AddFile("Logs/GeoPointAPI - {Date}.txt");
            }
            else
            {
                app.UseHsts();
            }
            app.UseSwaggerDocumentation();
            app.UseCors("CorsPolicy");
            app.UseIpRateLimiting();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseRewriter(new RewriteOptions().AddRedirectToHttps(301, 44343));
            app.UseSignalR(routes => { routes.MapHub<friendRequest>("/friendRequest"); });
            app.UseMvc();

            seedMongo.initDatabase(150).Wait();
        }
    }
}
