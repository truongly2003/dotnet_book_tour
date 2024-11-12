using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookTour.Domain.Entity;

public partial class Role
{
    [Column("role_id")]
    public int RoleId { get; set; }
    [Column("role_name")]
    public string RoleName { get; set; } = null!;

    public virtual ICollection<Roleoperation> Roleoperations { get; set; } = new List<Roleoperation>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
