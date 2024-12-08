using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookTour.Domain.Entity;

public partial class Employee
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("employee_id")]

    public string EmployeeId { get; set; } = null!;

    [Column("employee_email")]
    public string EmployeeEmail { get; set; } = null!;

    [ForeignKey("UserId")]
    [Column("user_id")]
    public int? UserId { get; set; }

    public virtual User? User { get; set; }
}
