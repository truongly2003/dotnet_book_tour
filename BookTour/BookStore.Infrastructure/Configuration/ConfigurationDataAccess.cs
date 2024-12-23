﻿using BookStore.DataAccess;
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
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IFeedbackRepository, FeedbackRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPassengerRepository, PassengerRepository>();
            services.AddScoped<IDecentralizationRepository, DecentralizationRepository>();

            // 2 service
            services.AddScoped<IRouteService, RouteService>();
            services.AddScoped<IDepartureService, DepartureService>();
            services.AddScoped<IArrivalService, ArrivalService>();
            services.AddScoped<IDetailRouteService, DetailRouteService>();
            services.AddScoped<ILegService, LegService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IDecentralizationService, DecentralizationService>();
            services.AddScoped<IVNPayService,VNPayService>();
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