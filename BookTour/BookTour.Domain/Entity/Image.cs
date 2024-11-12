using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookTour.Domain.Entity;

public partial class Image
{
    [Column("image_id")]
    public int ImageId { get; set; }
    [Column("text_image")]
    public string? TextImage { get; set; }
    [Column("detail_route_id")]
    public int DetailRouteId { get; set; }
    [Column("is_primary")]
    public int IsPrimary { get; set; }

    public virtual Detailroute DetailRoute { get; set; } = null!;
}
