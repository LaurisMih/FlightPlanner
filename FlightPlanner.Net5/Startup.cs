using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using FlightPlanner.Net5.Filters;
using Microsoft.AspNetCore.Authentication;
using FlightPlanner.Net5;
using Microsoft.EntityFrameworkCore;
using FlightPlanner.Data;
using FlightPlanner.Core.Services;
using FlightPlanner.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;
using FlightPlanner.Core.Validations;
using AutoMapper;
using FlightPlanner.Core.Models;

namespace FlightPlannerNet5
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FlightPlanner", Version = "v1" });
            });
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthorizationHandler>("BasicAuthentication", null);

            services.AddDbContext<FlightPlannerDBContext>(options => options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")
                ));
            services.AddScoped<IDbService, DbService>();
            services.AddScoped<IEntityService<Airport>, EntityService<Airport>>();
            services.AddScoped<IEntityService<Flight>, EntityService<Flight>>();
            services.AddScoped<IFlightService, FlightService>();
            services.AddScoped<IAirportService, AirportService>();

            services.AddScoped<IFlightValidator, CarrierValidator>();
            services.AddScoped<IFlightValidator, FlightTimeValidator>();
            services.AddScoped<IFlightValidator, FlightAirportValidator>();
            services.AddScoped<IAirportValidator, AirportCityValidator>();
            services.AddScoped<IAirportValidator, AirportCodeValidator>();
            services.AddScoped<IAirportValidator, AirportCountryValidator>();

            services.AddScoped<IFlightPlannerDbContext, FlightPlannerDBContext>();
            services.AddSingleton<IMapper>(AutoMapperConfig.CreateMapper());
            services.AddScoped<IEntityService<User>, EntityService<User>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FlightPlanner v1"));
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
