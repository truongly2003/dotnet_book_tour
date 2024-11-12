using System;
using System.Collections.Generic;

namespace BookTour.Domain.Entity;

public partial class Roleoperation
{
    public int RoleOperationId { get; set; }

    public int? RoleId { get; set; }

    public int? PermissionId { get; set; }

    public int? OperationId { get; set; }

    public int? StatusId { get; set; }

    public virtual Operation? Operation { get; set; }

    public virtual Permission? Permission { get; set; }

    public virtual Role? Role { get; set; }
}
