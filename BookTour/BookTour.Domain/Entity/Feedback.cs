using System;
using System.Collections.Generic;

namespace BookTour.Domain.Entity;

public partial class Feedback
{
    public int FeedbackId { get; set; }

    public int? BookingId { get; set; }

    public int? DetailRouteId { get; set; }

    public string Text { get; set; } = null!;

    public float Rating { get; set; }

    public DateTime DateCreate { get; set; }

    public virtual Booking? Booking { get; set; }

    public virtual Detailroute? DetailRoute { get; set; }
}
