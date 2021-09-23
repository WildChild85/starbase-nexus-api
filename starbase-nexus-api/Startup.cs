using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using starbase_nexus_api.DbContexts;
using starbase_nexus_api.Entities.Identity;
using starbase_nexus_api.Entities.InGame;
using starbase_nexus_api.Entities.Knowledge;
using starbase_nexus_api.Entities.Social;
using starbase_nexus_api.Entities.Yolol;
using starbase_nexus_api.Filters.Api;
using starbase_nexus_api.Hubs;
using starbase_nexus_api.Models.Authentication;
using starbase_nexus_api.Models.Authentication.Discord;
using starbase_nexus_api.Models.Cdn;
using starbase_nexus_api.Models.Pwa;
using starbase_nexus_api.Repositories.Authentication;
using starbase_nexus_api.Repositories.Identity;
using starbase_nexus_api.Repositories.InGame;
using starbase_nexus_api.Repositories.Knowledge;
using starbase_nexus_api.Repositories.Social;
using starbase_nexus_api.Repositories.Yolol;
using starbase_nexus_api.Services.Authentication;
using starbase_nexus_api.Services.Cdn;
using starbase_nexus_api.Services.Media;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace starbase_nexus_api
{
    public class Startup
    {
        public const string API_NAME = "C# Rest Api for Starbase Nexus";
        public const string API_VERSION = "1.0.0";

        public const string API_DESCRIPTION = @"Nothing yet";

        public const string DEVELOPER_EMAIL = "wildchild85@protonmail.com";
        public const string DEVELOPER_FULL_NAME = "WildChild85";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // db setup
            string connectionString = Configuration.GetConnectionString("Mysql8Server");
            MySqlServerVersion serverVersion = new MySqlServerVersion(new Version(8, 0, 23));
            services.AddDbContext<MainDbContext>(
               options => options.UseLazyLoadingProxies().UseMySql(
                    connectionString,
                    serverVersion,
                    mysqlOptions =>
                    {
                        // mysqlOptions.CharSetBehavior(CharSetBehavior.NeverAppend);
                        mysqlOptions.UseNetTopologySuite();
                        mysqlOptions.EnableRetryOnFailure();
                    }
            ));

            // authentication
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<MainDbContext>()
                .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;

                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(24);
                options.Lockout.AllowedForNewUsers = true;

                options.User.RequireUniqueEmail = true;
                //options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/ ";
                options.User.AllowedUserNameCharacters = null;

                options.SignIn.RequireConfirmedAccount = true;
                options.SignIn.RequireConfirmedEmail = true;
            });
            string issuer = Configuration["JWTAuth:Issuer"] ?? throw new NullReferenceException("Configuration:JWTAuth:Issuer");
            string audience = Configuration["JWTAuth:Audience"] ?? throw new NullReferenceException("Configuration:JWTAuth:Audience");
            string key = Configuration["JWTAuth:Key"] ?? throw new NullReferenceException("Configuration:JWTAuth:Key");
            services.AddAuthentication(cfg =>
            {
                cfg.DefaultScheme = IdentityConstants.ApplicationScheme;
                cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
            )
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                    };
                });

            services.AddAuthorization(options =>
            {
                // setup policies here
            });

            // entity dto mapping
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // controllers
            services.AddControllers(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;
                setupAction.InputFormatters.Insert(0, GetJsonPatchInputFormatter());
            })
                .AddJsonOptions(setupAction =>
                {
                    setupAction.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    setupAction.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                })
                .AddNewtonsoftJson(setupAction =>
                {
                    setupAction.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                })
                .AddXmlDataContractSerializerFormatters()
            ;

            // register configurations
            services.Configure<JwtTokenOptions>(Configuration.GetSection("JWTAuth"));
            services.Configure<PwaOptions>(Configuration.GetSection("Pwa"));
            services.Configure<DiscordOptions>(Configuration.GetSection("Discord"));
            services.Configure<CdnOptions>(Configuration.GetSection("Cdn"));

            // repositories
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMaterialCategoryRepository<MaterialCategory>, MaterialCategoryRepository>();
            services.AddScoped<IMaterialRepository<Material>, MaterialRepository>();
            services.AddScoped<IItemCategoryRepository<ItemCategory>, ItemCategoryRepository>();
            services.AddScoped<IItemRepository<Item>, ItemRepository>();
            services.AddScoped<IYololProjectRepository<YololProject>, YololProjectRepository>();
            services.AddScoped<IYololScriptRepository<YololScript>, YololScriptRepository>();
            services.AddScoped<ILikeRepository<Like>, LikeRepository>();
            services.AddScoped<ICompanyRepository<Company>, CompanyRepository>();
            services.AddScoped<IGuideRepository<Guide>, GuideRepository>();

            // services
            services.AddScoped<IAccessTokenService, AccessTokenService>();
            services.AddScoped<IDiscordService, DiscordService>();
            services.AddScoped<ICdnService, CdnService>();
            services.AddScoped<IImageService, ImageService>();


            services.AddSwaggerGen(c =>
            {
                OpenApiInfo docs = new OpenApiInfo
                {
                    Title = API_NAME,
                    Version = API_VERSION,
                    Description = API_DESCRIPTION,
                    Contact = new OpenApiContact
                    {
                        Email = DEVELOPER_EMAIL,
                        Name = DEVELOPER_FULL_NAME,
                    },
                    License = new OpenApiLicense
                    {
                        Name = "none"
                    }
                };
                c.SwaggerDoc("v1", docs);
                // Swagbuckle uses the class name for unique scheme ids. This will use the whole namespace.
                c.CustomSchemaIds(x => x.FullName);
                // To make json patch document work with swagbuckle, use this:
                // https://michael-mckenna.com/swagger-with-asp-net-core-3-1-json-patch/
                c.DocumentFilter<JsonPatchDocumentFilter>();

                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddHttpClient();

            services.AddControllers();

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Necessary when running under an nginx proxy
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{API_NAME} - v{API_VERSION}");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseExceptionHandler(config =>
                {
                    config.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "application/json";

                        IExceptionHandlerFeature error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            string jsonString = JsonSerializer.Serialize(error.Error);

                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });
            }

            bool updateDatabaseEnabled = Configuration.GetValue<bool>("UpdateDatabase");
            if (updateDatabaseEnabled)
            {
                UpdateDatabase(app);
            }

            string pwaUri = Configuration.GetValue<string>("Pwa:Uri");
            app.UseCors(
                options =>
                {
                    options.WithOrigins(pwaUri);
                    options.WithMethods("GET", "POST", "PUT", "PATCH", "DELETE", "OPTIONS");
                    options.AllowAnyHeader();
                    options.AllowCredentials();
                    options.SetIsOriginAllowed((host) => true);
                    options.WithExposedHeaders(new string[]
                    {
                    "Pagination.TotalCount",
                    "Pagination.PageSize",
                    "Pagination.Page",
                    "Pagination.TotalPages"
                    });
                });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ViewsHub>("hubs/views");
            });
        }
        private static NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter()
        {
            ServiceProvider builder = new ServiceCollection()
                .AddLogging()
                .AddMvc()
                .AddNewtonsoftJson()
                .Services.BuildServiceProvider();

            return builder
                .GetRequiredService<IOptions<MvcOptions>>()
                .Value
                .InputFormatters
                .OfType<NewtonsoftJsonPatchInputFormatter>()
                .First();
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (IServiceScope serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (MainDbContext context = serviceScope.ServiceProvider.GetService<MainDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
