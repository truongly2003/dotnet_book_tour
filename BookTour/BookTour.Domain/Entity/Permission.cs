using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookTour.Domain.Entity;

public partial class Permission
{
    [Column("permission_id")]
    public int PermissionId { get; set; }
    [Column("permission_name")]
    public string PermissionName { get; set; } = null!;

    public virtual ICollection<Roleoperation> Roleoperations { get; set; } = new List<Roleoperation>();
}
