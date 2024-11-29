using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookTour.Domain.Entity;

public partial class Detailroute
{
    [Column("detail_route_id")]
    public int DetailRouteId { get; set; }
    [Column("route_id")]
    public int RouteId { get; set; }
    [Column("price")]
    public double Price { get; set; }
    [Column("detail_route_name")]
    public string DetailRouteName { get; set; }
    [Column("description")]
    public string Description { get; set; }
    [Column("time_to_departure")]
    public DateOnly TimeToDeparture { get; set; }
    [Column("time_to_finish")]
    public DateOnly TimeToFinish { get; set; }
    [Column("stock")]
    public int BookInAdvance { get; set; }
    [Column("book_in_advance")]
        
    public int Stock { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } 

    public virtual ICollection<Feedback> Feedbacks { get; set; } 

    public virtual ICollection<Image> Images { get; set; } 

    public virtual ICollection<Leg> Legs { get; set; } 

    public virtual Route Route { get; set; }
}
