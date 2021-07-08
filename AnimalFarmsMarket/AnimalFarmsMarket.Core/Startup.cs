using AnimalFarmsMarket.Core.Extensions;
using AnimalFarmsMarket.Data;
using AnimalFarmsMarket.Data.Services.Implemantations;
using AnimalFarmsMarket.Data.Services.Interfaces;
using AnimalFarmsMarket.Data.Services.Implementations;
using AnimalFarmsMarket.Data.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

using System;
using AnimalFarmsMarket.Data.Profiles;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace AnimalFarmsMarket.Core
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                
            });
            services.AddAuthorization(option =>
            {
                option.AddPolicy("CustomerRolePolicy", policy => policy.RequireRole("Customer"));
                option.AddPolicy("AdminAndDeliveryRolePolicy", policy => policy.RequireAssertion(x=>x.User.IsInRole("Admin")||x.User.IsInRole("Delivery")));


            });

            services.AddScoped<ILiveStockService, LiveStockService>();

            services.AddDbContextAndConfigurations(Environment, Configuration);
            services.AddSession();
            
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IRatingService, RatingService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<ILivestoclImage, LivestockImageService>();
            services.AddScoped<IFileUpload, FileUpload>();

            
            services.Configure<CloudinaryConfig>(Configuration.GetSection("CloudinaryConfig"));
            services.Configure<JWTData>(Configuration.GetSection(JWTData.Data));
            services.Configure<EmailSenderConfig>(Configuration.GetSection(EmailSenderConfig.ConfigSection));
            services.AddAutoMapper(typeof(MappingProfile));
            services.Configure<DataProtectionTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromHours(5));
            services.AddScoped<IDeliveryAssignmentService, DeliveryAssignmentService>();
            services.AddScoped<ITrackingService, TrackingService>();
            services.AddScoped<IOrderService, Orderservice>();
            services.AddScoped<IOrderItemsService, OrderItemsService>();
            services.AddScoped<IMarketService, MarketService>();

            //Password complexity and confirm email configuration
            services.Configure<IdentityOptions>(options =>
            {
                //Require a user to confirm email befor sining in
                options.SignIn.RequireConfirmedEmail = true;

                //Require each user to have a uniqe email
                options.User.RequireUniqueEmail = true;

                //Password configuratiosn
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
            });

            //configuration for JWT
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(Configuration.GetSection("JWTConfigurations:SecretKey").Value)),
                    ValidateIssuer = true,
                    ValidIssuer = Configuration.GetSection("JWTConfigurations:Issuer").Value,
                    ValidateAudience = true,
                    ValidAudience = Configuration.GetSection("JWTConfigurations:Audience").Value
                };
            })
            .AddCookie(config =>
            {
                config.ForwardAuthenticate = CookieAuthenticationDefaults.AuthenticationScheme;

            }).AddGoogle(options =>
                {
                    options.ClientId = Configuration["GoogleConfig:ClientId"];
                    options.ClientSecret = Configuration["GoogleConfig:ClientSecret"];
                    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.Events.OnRemoteFailure = (context) =>
                    {
                        context.Response.Redirect("/api/v1/Auth/google-signin");
                        context.HandleResponse();
                        return System.Threading.Tasks.Task.CompletedTask;
                    };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();
            app.UseSession();
            app.UseCookiePolicy(new CookiePolicyOptions()
            {
                MinimumSameSitePolicy = SameSiteMode.Lax
            });
            app.Use(async (context, next) =>
            {
                var token = context.Session.GetString("Token");
                if (!string.IsNullOrWhiteSpace(token))
                    context.Request.Headers.Add("Authorization", "Bearer " + token);
                await next();
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            Preseeder.EnsurePopulated(app);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "details",
                    pattern: "{controller=Market}/{action=Details}/{id}"
                    );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
