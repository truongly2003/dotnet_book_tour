using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookTour.Domain.Entity;

public partial class Customer
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("customer_id")]
    public int CustomerId { get; set; }

    [Required]
    [Column("customer_name")]
    public string CustomerName { get; set; } = null!;

    [Required]
    [EmailAddress]
    [Column("customer_email")]
    public string CustomerEmail { get; set; } = null!;

    [Column("customer_address")]
    public string? CustomerAddress { get; set; }

    [Required]
    [Column("customer_phone")]
    public string CustomerPhone { get; set; } = null!;

    [ForeignKey("UserId")]
    [Column("user_id")]
    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
