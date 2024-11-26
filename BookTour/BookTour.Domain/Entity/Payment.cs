using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookTour.Domain.Entity;

public partial class Payment
{

    [Column("payment_id")]
    public int PaymentId { get; set; }

    [Column("payment_name")]
    public string? PaymentName { get; set; }

    [Column("payment_date")]
    public DateTime PaymentDate { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
