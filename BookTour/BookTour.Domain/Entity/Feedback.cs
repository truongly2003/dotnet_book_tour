using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookTour.Domain.Entity;

public partial class Feedback
{
    [Column("feedback_id")]
    public int FeedbackId { get; set; }
    [Column("booking_id")]
    public int BookingId { get; set; }
    [Column("detail_route_id")]
    public int DetailRouteId { get; set; }
    [Column("text")]
    public string Text { get; set; } = null!;
    [Column("rating")]
    public float Rating { get; set; }
    [Column("date_create")]
    public DateOnly DateCreate { get; set; }

    public virtual Booking Booking { get; set; }

    public virtual Detailroute DetailRoute { get; set; }
}
