using System;
using System.Collections.Generic;

namespace BookTour.Domain.Entity;

public partial class Operation
{
    public int OperationId { get; set; }

    public string OperationName { get; set; } = null!;

    public virtual ICollection<Roleoperation> Roleoperations { get; set; } = new List<Roleoperation>();
}
