using System;
using System.Collections.Generic;

namespace BookTour.Domain.Entity;

public partial class Permission
{
    public int PermissionId { get; set; }

    public string PermissionName { get; set; } = null!;

    public virtual ICollection<Roleoperation> Roleoperations { get; set; } = new List<Roleoperation>();
}
