using Common.Logging;
using HealthChecks.UI.Client;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Order.Persistence.Database;
using Order.Service.Proxies;
using Order.Service.Proxies.Catalog;
using Order.Service.Queries;
using System.Reflection;
using System.Text;

namespace Order.Api
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
            // HttpContextAccessor
            services.AddHttpContextAccessor();

            // Database Configuration
            services.AddDbContext<OrderDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Health Checks
            services.AddHealthChecks()
                .AddDbContextCheck<OrderDbContext>(typeof(OrderDbContext).Name);
            // Health Checks UI
            services.AddHealthChecksUI().AddInMemoryStorage();

            // Api Urls
            services.Configure<ApiUrls>(
                options => Configuration.GetSection("ApiUrls").Bind(options));

            // Proxies
            services.AddHttpClient<ICatalogProxy, CatalogProxy>();

            // Command Services
            services.AddMediatR(Assembly.Load("Order.Service.EventHandlers"));

            // Query Services
            services.AddTransient<IOrderQueryService, OrderQueryService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Order.Api", Version = "v1" });
            });

            // Add Authentication
            var secretKey = Encoding.ASCII.GetBytes(
                Configuration.GetValue<string>("SecretKey")
            );

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order.Api v1"));
            }
            else
            {

            }

            loggerFactory.AddSyslog(
                    Configuration.GetValue<string>("Papertrail:host"),
                    Configuration.GetValue<int>("Papertrail:port"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                
                endpoints.MapHealthChecksUI();

                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });
        }
    }
}
