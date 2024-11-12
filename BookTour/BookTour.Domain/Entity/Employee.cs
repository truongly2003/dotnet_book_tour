using System;
using System.Collections.Generic;

namespace BookTour.Domain.Entity;

public partial class Employee
{
    public string EmployeeId { get; set; } = null!;

    public string EmployeeEmail { get; set; } = null!;

    public int? UserId { get; set; }

    public virtual User? User { get; set; }
}
