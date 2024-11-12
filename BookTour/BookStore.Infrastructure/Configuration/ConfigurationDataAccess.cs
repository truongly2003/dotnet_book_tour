using BookStore.DataAccess;
using BookStore.DataAccess.Repository;
using BookTour.Application.Interface;
using BookTour.Application.Service;
using BookTour.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Configuration
{
    public static class ConfigurationDataAccess
    {
        public static void RegisterDB(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                                    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            services.AddDbContext<BookTourDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        }


        public static void RegisterDI(this IServiceCollection services)
        {
            // 1 repository
            services.AddScoped<IRouteRepository, RouteRepository>();
            services.AddScoped<IDepartureRepository, DepartureRepository>();
            services.AddScoped<IArrivalRepository, ArrivalRepository>();
            services.AddScoped<IDetailRouteRepository, DetailRouteRepository>();
            services.AddScoped<ILegRepository, LegRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            // 2 service
            services.AddScoped<IRouteService, RouteService>();
            services.AddScoped<IDepartureService, DepartureService>();
            services.AddScoped<IArrivalService, ArrivalService>();
            services.AddScoped<IDetailRouteService, DetailRouteService>();
            services.AddScoped<ILegService, LegService>();
            services.AddScoped<IImageService, ImageService>();
        }
        public static void ConfigApi(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("http://localhost:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });
        }
    }
}
