using System;
using System.Collections.Generic;

namespace BookTour.Domain.Entity;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? IdentityId { get; set; }

    public int? RoleId { get; set; }

    public string Email { get; set; } = null!;

    public int Status { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual Role? Role { get; set; }
}
