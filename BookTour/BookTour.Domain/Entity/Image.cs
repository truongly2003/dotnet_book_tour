using System;
using System.Collections.Generic;

namespace BookTour.Domain.Entity;

public partial class Image
{
    public int ImageId { get; set; }

    public string? TextImage { get; set; }

    public int DetailRouteId { get; set; }

    public int? IsPrimary { get; set; }

    public virtual Detailroute DetailRoute { get; set; } = null!;
}
