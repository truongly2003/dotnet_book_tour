using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookTour.Domain.Entity;

public partial class Paymentstatus
{

    [Column("payment_status_id")]
    public int PaymentStatusId { get; set; }

    [Column("status_name")]
    public string StatusName { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
