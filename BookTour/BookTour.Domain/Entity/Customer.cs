using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookTour.Domain.Entity;

public partial class Customer
{
    [Column("customer_id")]
    public int CustomerId { get; set; }
    [Column("customer_name")]
    public string CustomerName { get; set; } = null!;
    [Column("customer_email")]
    public string CustomerEmail { get; set; } = null!;
    [Column("customer_address")]
    public string CustomerAddress { get; set; } = null!;
    [Column("user_id")]
    public int UserId { get; set; }

    [Column("customer_phone")]
    public string CustomerPhone { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual User User { get; set; }
}
