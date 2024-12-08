using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookTour.Domain.Entity;

public partial class Roleoperation
{
    [Column("role_operation_id")]
    public int RoleOperationId { get; set; }
    [Column("role_id")]

    public int? RoleId { get; set; }
    [Column("permission_id")]
    public int? PermissionId { get; set; }
    [Column("operation_id")]

    public int? OperationId { get; set; }
    [Column("status_id")]
    public int? StatusId { get; set; }

    public virtual Operation? Operation { get; set; }

    public virtual Permission? Permission { get; set; }

    public virtual Role? Role { get; set; }
}
