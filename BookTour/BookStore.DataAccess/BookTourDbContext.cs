using BookTour.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess
{
    public class BookTourDbContext : DbContext
    {
        public BookTourDbContext(DbContextOptions<BookTourDbContext> options) : base(options)
        {
        }

        // Các DbSet cho các entity
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Arrival> Arrivals { get; set; }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Departure> Departures { get; set; }
        public DbSet<Detailroute> Detailroutes { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Feedback> Feedback { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Leg> Legs { get; set; }
        public DbSet<Objects> Objects { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Paymentstatus> Paymentstatus { get; set; }
        public DbSet<Permission> Permissions { get; set; }
       
        public DbSet<Roleoperation> Roleoperations { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Statusroleoperation> Statusroleoperation { get; set; }
        public DbSet<Ticket> Tickets { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Ticket>()
         .HasKey(t => new { t.BookingId, t.PassengerId });
        }

        public async Task<Dictionary<string, int>> GetStatisticsAsync()
        {
            var statistics = new Dictionary<string, int>
            {   
                { "Users", await Users.CountAsync() },
                { "Roles", await Roles.CountAsync() },
                { "Customers", await Customers.CountAsync() },
                { "Arrivals", await Arrivals.CountAsync() },
                { "Bookings", await Bookings.CountAsync() },
                { "Departures", await Departures.CountAsync() },
                { "Detailroutes", await Detailroutes.CountAsync() },
                { "Employees", await Employees.CountAsync() },
                { "Feedback", await Feedback.CountAsync() },
                { "Images", await Images.CountAsync() },
                { "Legs", await Legs.CountAsync() },
                { "Objects", await Objects.CountAsync() },
                { "Operations", await Operations.CountAsync() },
                { "Passengers", await Passengers.CountAsync() },
                { "Payments", await Payments.CountAsync() },
                { "Paymentstatus", await Paymentstatus.CountAsync() },
                { "Permissions", await Permissions.CountAsync() },
                { "Roleoperations", await Roleoperations.CountAsync() },
                { "Routes", await Routes.CountAsync() },
                { "Statusroleoperation", await Statusroleoperation.CountAsync() },
                { "Tickets", await Tickets.CountAsync() }
            };

            return statistics;
        }

        // Thống kê số lượng booking theo từng trạng thái thanh toán
        public async Task<List<BookingPaymentStatusStatistics>> GetBookingStatisticsByPaymentStatusAsync()
        {
            var result = await Bookings
                .Join(Paymentstatus, b => b.PaymentStatusId, ps => ps.PaymentStatusId, (b, ps) => new { b, ps })
                .GroupBy(x => x.ps.StatusName)
                .Select(g => new BookingPaymentStatusStatistics
                {
                    StatusName = g.Key,
                    TotalBookings = g.Count(),
                    TotalRevenue = g.Sum(x => x.b.TotalPrice)
                })
                .ToListAsync();

            return result;
        }

        public class BookingPaymentStatusStatistics
        {
            public string StatusName { get; set; }
            public int TotalBookings { get; set; }
            public decimal? TotalRevenue { get; set; }
        }

        // Thống kê doanh thu theo từng tháng
        public async Task<List<MonthlyRevenueStatistics>> GetMonthlyRevenueStatisticsAsync()
        {
            var result = await Bookings
                .GroupBy(b => new { Year = b.TimeToOrder.Year, Month = b.TimeToOrder.Month })
                .Select(g => new MonthlyRevenueStatistics
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalBookings = g.Count(),
                    TotalRevenue = g.Sum(b => b.TotalPrice)
                })
                .OrderByDescending(mrs => mrs.Year)
                .ThenByDescending(mrs => mrs.Month)
                .ToListAsync();

            return result;
        }

        public class MonthlyRevenueStatistics
        {
            public int Year { get; set; }
            public int Month { get; set; }
            public int TotalBookings { get; set; }
            public decimal? TotalRevenue { get; set; }
        }

        
        // Thống kê Tôur phổ biến
        public async Task<List<PopularTourStatistics>> GetPopularTourStatisticsAsync()
        {
            var result = await Detailroutes
                .GroupJoin(Bookings, dr => dr.DetailRouteId, b => b.DetailRouteId, (dr, bookings) => new { dr, bookings })
                .SelectMany(
                    x => x.bookings.DefaultIfEmpty(),
                    (x, b) => new { x.dr, b }
                )
                .GroupJoin(Feedback, x => x.dr.DetailRouteId, f => f.DetailRouteId, (x, feedbacks) => new { x.dr, x.b, feedbacks })
                .SelectMany(
                    x => x.feedbacks.DefaultIfEmpty(),
                    (x, f) => new { x.dr, x.b, f }
                )
                .GroupBy(x => new { x.dr.DetailRouteId, x.dr.DetailRouteName })
                .Select(g => new PopularTourStatistics
                {
                    DetailRouteName = g.Key.DetailRouteName,
                    TotalBookings = g.Count(x => x.b != null),
                    TotalRevenue = g.Sum(x => x.b != null ? x.b.TotalPrice : 0),
                    AverageRating = g.Average(x => x.f != null ? x.f.Rating : 0)
                })
                .OrderByDescending(p => p.TotalBookings)
                .ToListAsync();

            return result;
        }

        public class PopularTourStatistics
        {
            public string DetailRouteName { get; set; }
            public int TotalBookings { get; set; }
            public decimal? TotalRevenue { get; set; }
            public double? AverageRating { get; set; }
        }


        // Thôngs kê khách hàng theo soo lan dat tour
        public async Task<List<CustomerBookingStatistics>> GetCustomerBookingStatisticsAsync()
        {
            var result = await Customers
                .GroupJoin(Bookings, c => c.CustomerId, b => b.CustomerId, (c, bookings) => new { c, bookings })
                .SelectMany(
                    x => x.bookings.DefaultIfEmpty(),
                    (x, b) => new { x.c, b }
                )
                .GroupBy(x => new { x.c.CustomerId, x.c.CustomerName })
                .Select(g => new CustomerBookingStatistics
                {
                    CustomerName = g.Key.CustomerName,
                    TotalBookings = g.Count(x => x.b != null),
                    TotalSpent = g.Sum(x => x.b != null ? x.b.TotalPrice : 0)
                })
                .OrderByDescending(cbs => cbs.TotalBookings)
                .ToListAsync();

            return result;
        }

        public class CustomerBookingStatistics
        {
            public string CustomerName { get; set; }
            public int TotalBookings { get; set; }
            public decimal? TotalSpent { get; set; }
        }

        // Thống kê rating trung bình các tour Thịnh đẹp trai
        public async Task<List<TourRatingStatistics>> GetTourRatingStatisticsAsync()
        {
            var result = await Detailroutes
                .GroupJoin(Feedback, dr => dr.DetailRouteId, f => f.DetailRouteId, (dr, feedbacks) => new { dr, feedbacks })
                .SelectMany(
                    x => x.feedbacks.DefaultIfEmpty(),
                    (x, f) => new { x.dr, f }
                )
                .GroupBy(x => new { x.dr.DetailRouteId, x.dr.DetailRouteName })
                .Select(g => new TourRatingStatistics
                {
                    DetailRouteName = g.Key.DetailRouteName,
                    TotalFeedback = g.Count(x => x.f != null),
                    AverageRating = g.Average(x => x.f != null ? x.f.Rating : 0),
                    LowestRating = g.Min(x => x.f != null ? x.f.Rating : (double?)null),
                    HighestRating = g.Max(x => x.f != null ? x.f.Rating : (double?)null)
                })
                .Where(trs => trs.TotalFeedback > 0)
                .OrderByDescending(trs => trs.AverageRating)
                .ToListAsync();

            return result;
        }

        public class TourRatingStatistics
        {
            public string DetailRouteName { get; set; }
            public int TotalFeedback { get; set; }
            public double? AverageRating { get; set; }
            public double? LowestRating { get; set; }
            public double? HighestRating { get; set; }
        }

        // Thống kê các tuyến đường đẹp trai của Thịnh
        public async Task<List<RouteTourStatistics>> GetRouteTourStatisticsAsync()
        {
            var result = await Routes
                .Join(Departures, r => r.DepartureId, d => d.DepartureId, (r, d) => new { r, d })
                .Join(Arrivals, rd => rd.r.ArrivalId, a => a.ArrivalId, (rd, a) => new { rd.r, rd.d, a })
                .Join(Detailroutes, rda => rda.r.RouteId, dr => dr.RouteId, (rda, dr) => new { rda.r, rda.d, rda.a, dr })
                .GroupBy(x => new { x.r.RouteId, x.d.DepartureName, x.a.ArrivalName })
                .Select(g => new RouteTourStatistics
                {
                    DepartureName = g.Key.DepartureName,
                    ArrivalName = g.Key.ArrivalName,
                    TotalRoutes = g.Count(),
                    AveragePrice = (decimal?)g.Average(x => x.dr.Price)
                })
                .ToListAsync();

            return result;
        }

        public class RouteTourStatistics
        {
            public string DepartureName { get; set; }
            public string ArrivalName { get; set; }
            public int TotalRoutes { get; set; }
            public decimal? AveragePrice { get; set; }
        }

        // Thống kê loại khách
        public async Task<List<PassengerTypeStatistics>> GetPassengerTypeStatisticsAsync()
        {
            var result = await Passengers
                .Join(Objects, p => p.ObjectId, o => o.ObjectId, (p, o) => new { p, o })
                .GroupBy(x => new { x.o.ObjectId, x.o.ObjectName })
                .Select(g => new PassengerTypeStatistics
                {
                    ObjectName = g.Key.ObjectName,
                    TotalPassengers = g.Count()
                })
                .ToListAsync();

            return result;
        }

        public class PassengerTypeStatistics
        {
            public string ObjectName { get; set; }
            public int TotalPassengers { get; set; }
        }


        //thống kê độ tuổi khách hàng
        public async Task<List<PassengerAgeGroupStatistics>> GetPassengerAgeGroupStatisticsAsync()
        {
            var passengers = await Passengers.ToListAsync();

            var result = passengers
                .GroupBy(p => new
                {
                    AgeGroup = DateTime.Now.Year - p.DateBirth.Year < 12 ? "Trẻ em" :
                               DateTime.Now.Year - p.DateBirth.Year < 18 ? "Thanh thiếu niên" :
                               DateTime.Now.Year - p.DateBirth.Year < 60 ? "Người lớn" :
                               "Người cao tuổi"
                })
                .Select(g => new PassengerAgeGroupStatistics
                {
                    AgeGroup = g.Key.AgeGroup,
                    Total = g.Count()
                })
                .ToList();

            return result;
        }



        public class PassengerAgeGroupStatistics
        {
            public string AgeGroup { get; set; }
            public int Total { get; set; }
        }
    }
}
   
    