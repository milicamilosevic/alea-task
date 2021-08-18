using Alea_Books_API.Web.Data;
using Alea_Books_API.Web.Data.Models;
using Alea_Books_API.Web.Services;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alea_Books_API.Web
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
            services.AddDbContext<BooksDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("BooksDatabase"));
            });

            services.AddIdentityCore<IdentityUser>(options =>
            {
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequiredLength = 8;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;
                options.Lockout.AllowedForNewUsers = false;
            })
            .AddEntityFrameworkStores<BooksDbContext>()
            .AddSignInManager<SignInManager<IdentityUser>>()
            .AddDefaultTokenProviders();

            //Authentication
            var secret = Configuration["TokenConstants:Secret"]; //TODO: Use user secrets for this
            var secretBytes = Encoding.UTF8.GetBytes(secret);
            var key = new SymmetricSecurityKey(secretBytes);
            services.AddAuthentication("Jwt")
                .AddJwtBearer("Jwt", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                    };
                });

            //Authorization
            services.AddAuthorizationCore(options =>
            {
                var defaultAuthorizationBuilder = new AuthorizationPolicyBuilder();
                var defaultAuthorizationPolicy = defaultAuthorizationBuilder
                    .AddAuthenticationSchemes(new string[] { "Jwt" })
                    .RequireAuthenticatedUser()
                    .RequireClaim(JwtRegisteredClaimNames.Sub)
                    .RequireClaim(JwtRegisteredClaimNames.Email)
                    .RequireClaim(JwtRegisteredClaimNames.Iat)
                    .Build();
                options.DefaultPolicy = defaultAuthorizationPolicy;
            });

            services.AddSingleton(x => new BlobServiceClient(Configuration.GetConnectionString("AzureBlobStorage")));
            services.AddTransient<BlobService>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
