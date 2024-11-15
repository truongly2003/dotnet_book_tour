using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookTour.Domain.Entity;

public partial class Booking
{


    [Column("booking_id")]
    public int BookingId { get; set; }

    [Column("customer_id")]
    public int? CustomerId { get; set; }

    [Column("total_price")]
    public decimal? TotalPrice { get; set; }

    [Column("time_to_order")]
    public DateTime? TimeToOrder { get; set; }

    [Column("payment_id")]
    public int? PaymentId { get; set; }

    [Column("payment_status_id")]
    public int? PaymentStatusId { get; set; }

    [Column("status_booking")]
    public int StatusBooking { get; set; }

    [Column("detail_route_id")]
    public int? DetailRouteId { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Detailroute? DetailRoute { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual Payment? Payment { get; set; }

    public virtual Paymentstatus? PaymentStatus { get; set; }

    public virtual ICollection<Passenger> Passengers { get; set; } = new List<Passenger>();
}
