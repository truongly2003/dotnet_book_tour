using System;
using System.Collections.Generic;

namespace BookTour.Domain.Entity;

public partial class Role
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<Roleoperation> Roleoperations { get; set; } = new List<Roleoperation>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
