using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookTour.Domain.Entity;

public partial class Role
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("role_id")]
    public int RoleId { get; set; }
    [Column("role_name")]
    public string RoleName { get; set; } = null!;

    public virtual ICollection<Roleoperation> Roleoperations { get; set; } = new List<Roleoperation>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public static implicit operator Role(Task<Role> v)
    {
        throw new NotImplementedException();
    }
}
